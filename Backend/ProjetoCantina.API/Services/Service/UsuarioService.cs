using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjetoCantina.API.DTOs;
using ProjetoCantina.API.Models;
using ProjetoCantina.API.Services.Interfaces;
using ProjetoCantina.API.UnitOfWork;
using ProjetoCantina.API.Utils;

namespace ProjetoCantina.API.Services.Service;

public class UsuarioService : IUsuarioService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UsuarioService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<UsuarioDTO>?> GetAllUsuariosAsync()
    {
        var usuarios = await _unitOfWork
            .UsuarioRepository
                .GetAllAsync<Usuario>(
                    orderBy: u => u.OrderByDescending(u => u.UsuarioID)
                );

        return _mapper.Map<IEnumerable<UsuarioDTO>>(usuarios);
    }

    public async Task<IEnumerable<UsuarioDTO>?> GetAllUsuariosComCaixasAsync()
    {
        var usuarios = await _unitOfWork
            .UsuarioRepository
                .GetAllAsync<Usuario>(
                    orderBy: u => u.OrderBy(u => u.NomeUsuario),
                    includeProperties: "Caixas"
                );

        return _mapper.Map<IEnumerable<UsuarioDTO>>(usuarios);
    }

    public async Task<UsuarioDTO?> GetUsuarioByIdAsync(int usuarioID)
    {
        var usuario = await _unitOfWork
             .UsuarioRepository
                 .GetByIdAsync(
                    firstOrDefault: u => u.UsuarioID == usuarioID
                 );

        return _mapper.Map<UsuarioDTO>(usuario);
    }

    public async Task<UsuarioDTO?> GetUsuarioComCaixaByIdAsync(int usuarioID)
    {
        var usuario = await _unitOfWork
             .UsuarioRepository
                 .GetByIdAsync(
                    firstOrDefault: u => u.UsuarioID == usuarioID,
                    includeProperties: "Caixas"
                 );

        return _mapper.Map<UsuarioDTO>(usuario);
    }

    public async Task<IEnumerable<UsuarioDTO>?> GetUsuarioByNomeUsuarioAsync(string nomeUsuario)
    {
        var usuario = await _unitOfWork
            .UsuarioRepository
                .GetAllAsync<Usuario>(
                    where: u => EF.Functions.Like(u.NomeUsuario, $"%{nomeUsuario}%"),
                    orderBy: u => u.OrderBy(u => u.NomeUsuario)
                );

        return _mapper.Map<IEnumerable<UsuarioDTO>>(usuario);
    }

    public async Task<IEnumerable<UsuarioDTO>?> GetUsuarioComCaixasByNomeUsuarioAsync(string nomeUsuario)
    {
        var usuario = await _unitOfWork
            .UsuarioRepository
                .GetAllAsync<Usuario>(
                    where: u => EF.Functions.Like(u.NomeUsuario, $"%{nomeUsuario}%"),
                    includeProperties: "Caixas",
                    orderBy: u => u.OrderBy(u => u.NomeUsuario)
                );

        return _mapper.Map<IEnumerable<UsuarioDTO>>(usuario);
    }
    public async Task<bool> ValidarLoginUsuarioAsync(LoginUsuarioDTO loginUsuarioDTO)
    {
        var usuarioDTO = await _unitOfWork.UsuarioRepository
            .GetByIdAsync(
                firstOrDefault: u => u.NomeUsuario.Equals(loginUsuarioDTO.NomeUsuario)
            );

        if (usuarioDTO == null)
            return false;

        var validacaoSenha = PasswordHash.ValidatePassword(loginUsuarioDTO.Senha!, usuarioDTO.Senha!);

        return validacaoSenha;
    }

    public async Task<bool> InsertUsuarioaAsync(UsuarioDTO usuarioDTO)
    {
        var senhaCrip = PasswordHash.CreateHash(usuarioDTO.Senha!);
        usuarioDTO.Senha = senhaCrip;

        var usuario = _mapper.Map<Usuario>(usuarioDTO);

        var result = await _unitOfWork.UsuarioRepository.InsertAsync(usuario);

        if (result)
        {
            result = await _unitOfWork.SaveChangeAsync();
        }

        return result;
    }

    public async Task<bool> UpdateUsuarioAsync(UsuarioDTO usuarioDTO)
    {
        var senhaCrip = PasswordHash.CreateHash(usuarioDTO.Senha!);
        usuarioDTO.Senha = senhaCrip;

        var usuario = _mapper.Map<Usuario>(usuarioDTO);

        var result = _unitOfWork.UsuarioRepository.Update(usuario);

        if (result)
        {
            result = await _unitOfWork.SaveChangeAsync();
        }

        return result;
    }
    public async Task<bool> DeleteUsuarioAsync(int UsuarioID)
    {
        var result = await _unitOfWork.UsuarioRepository
            .DeleteAsync(
                firstOrDefault: u => u.UsuarioID == UsuarioID
            );

        if (result)
        {
            result = await _unitOfWork.SaveChangeAsync();
        }

        return result;
    }

}
