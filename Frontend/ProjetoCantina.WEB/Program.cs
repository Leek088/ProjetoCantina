using Microsoft.AspNetCore.Authentication.Cookies;
using ProjetoCantina.WEB.Hub;
using ProjetoCantina.WEB.Services.Interface;
using ProjetoCantina.WEB.Services.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

//Configuração para a ligação a API
builder.Services.AddHttpClient("ProjetoCantina.API", x =>
{
    x.BaseAddress = new Uri(builder.Configuration["ProjetoCantina.API:Uri"]!);
});

//Configuração para o Login e Autenticação

builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
        options.SlidingExpiration = true;
        options.AccessDeniedPath = "/Login/";
    });



//Injeção de dependencia dos serviços
builder.Services.AddScoped<ICategoriaService, CategoriaService>();
builder.Services.AddScoped<IProdutoService, ProdutoService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<ICaixaService, CaixaService>();
builder.Services.AddScoped<IFluxoCaixaService, FluxoCaixaService>();

//Configuracao para o HUB
builder.Services.AddSignalR();

var app = builder.Build();

//Configuração da politica de segurança para o Login
var cookiePolicyOptions = new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Strict,
};

app.UseCookiePolicy(cookiePolicyOptions);

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapHub<NotificacaoHub>("/notificacaoHub");

app.Run();
