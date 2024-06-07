using AutoMapper;
using ProjetoCantina.API.DTOs;
using ProjetoCantina.API.Models;
using ProjetoCantina.API.Services.Interfaces;
using ProjetoCantina.API.UnitOfWork;

namespace ProjetoCantina.API.Services.Service;

public class VendaService : IVendaService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public VendaService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<VendaDTO>?> GetAllVendasAsync()
    {
        var vendas = await _unitOfWork
            .VendaRepository
                .GetAllAsync<Venda>(
                    includeProperties: "UsuarioCaixa, Produto",
                    orderBy: v => v.OrderByDescending(v => v.VendaID)
                );

        return _mapper.Map<IEnumerable<VendaDTO>>(vendas);
    }

    public async Task<VendaDTO?> GetVendaByIdAsync(int vendaID)
    {
        var venda = await _unitOfWork
            .VendaRepository
                .GetByIdAsync(
                    includeProperties: "UsuarioCaixa, Produto",
                    firstOrDefault: v => v.VendaID == vendaID
                );

        return _mapper.Map<VendaDTO>(venda);
    }

    public async Task<bool> InsertVendaAsync(VendaDTO vendaDTO)
    {
        var venda = _mapper.Map<Venda>(vendaDTO);

        var result = await _unitOfWork.VendaRepository.InsertAsync(venda);

        if (result)
        {
            result = await _unitOfWork.SaveChangeAsync();
        }

        return result;
    }

    public async Task<bool> UpdateVendaAsync(VendaDTO vendaDTO)
    {
        var venda = _mapper.Map<Venda>(vendaDTO);

        var result = _unitOfWork.VendaRepository.Update(venda);

        if (result)
        {
            result = await _unitOfWork.SaveChangeAsync();
        }

        return result;
    }
    public async Task<bool> DeleteVendaAsync(int vendaID)
    {
        var result = await _unitOfWork
            .VendaRepository
                .DeleteAsync(
                    firstOrDefault: v => v.VendaID == vendaID
                );

        if (result)
        {
            result = await _unitOfWork.SaveChangeAsync();
        }

        return result;
    }
}
