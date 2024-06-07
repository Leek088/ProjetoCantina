using ProjetoCantina.API.Contexts;
using ProjetoCantina.API.Repositories.Interface;
using ProjetoCantina.API.Repositories.Repository;

namespace ProjetoCantina.API.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _appDbContext;

    public UnitOfWork(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    private CategoriaRepository? _categoriaRepository;
    private ProdutoRepository? _produtoRepository;
    private UsuarioRepository? _usuarioRepository;
    private CaixaRepository? _caixaRepository;
    private FluxoCaixaRepository? _fluxoCaixaRepository;
    private VendaRepository? _vendaRepository;

    public ICategoriaRepository CategoriaRepository
    {
        get
        {
            _categoriaRepository ??= new CategoriaRepository(_appDbContext);
            return _categoriaRepository;
        }
    }

    public IProdutoRepository ProdutoRepository
    {
        get
        {
            _produtoRepository ??= new ProdutoRepository(_appDbContext);
            return _produtoRepository;
        }
    }   

    public IUsuarioRepository UsuarioRepository
    {
        get
        {
            _usuarioRepository ??= new UsuarioRepository(_appDbContext);
            return _usuarioRepository;
        }
    }

    public ICaixaRepository CaixaRepository
    {
        get
        {
            _caixaRepository ??= new CaixaRepository(_appDbContext);
            return (_caixaRepository);
        }
    }

    public IFluxoCaixaRepository FluxoCaixaRepository
    {
        get
        {
            _fluxoCaixaRepository ??= new FluxoCaixaRepository(_appDbContext);
            return (_fluxoCaixaRepository);
        }
    }

    public IVendaRepository VendaRepository
    {
        get
        {
            _vendaRepository ??= new VendaRepository(_appDbContext);
            return (_vendaRepository);
        }
    }

    private bool disposed = false;
    public void Dispose(bool disposing)
    {
        if (!this.disposed)
        {
            if (disposing)
            {
                _appDbContext.Dispose();
            }
        }
        this.disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public async Task<bool> SaveChangeAsync()
    {
        try
        {
            await _appDbContext.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }
}
