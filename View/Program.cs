using Entities;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Hosting.Server;
using Services;
using Stripe;
using View.Authentication;
using View.Components;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.Configure<StripeSettings>(
    builder.Configuration.GetSection("Stripe")
);

// Set global API key
StripeConfiguration.ApiKey = builder.Configuration["Stripe:SecretKey"];

// Register payment service and interface
builder.Services.AddScoped<IStripePaymentService, StripePaymentService>();



builder.Services.AddScoped<IProductServices, ProductServices>();
builder.Services.AddSingleton<ICartService, CartService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IArtistService, ArtistService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<UserSession>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddAuthorizationCore();


builder.Services.AddServerSideBlazor()
    .AddCircuitOptions(options => { options.DetailedErrors = true; });


// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();



// Required for authentication system to access the HttpContext
builder.Services.AddHttpContextAccessor();

// Cascading state and authentication provider
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<AuthenticationStateProvider, ServerAuthenticationStateProvider>();

// Configure Authentication
builder.Services.AddAuthentication(AppConstants.AuthScheme)
    .AddCookie(AppConstants.AuthScheme, cookieOptions =>
    {
        cookieOptions.Cookie.Name = AppConstants.AuthScheme;
        cookieOptions.Cookie.SameSite = SameSiteMode.None;
        cookieOptions.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    })
    .AddGoogle(GoogleDefaults.AuthenticationScheme, googleOptions =>
    {
        googleOptions.ClientId = "";
        googleOptions.ClientSecret = "";
        googleOptions.AccessDeniedPath = "/access-denied";
        googleOptions.SignInScheme = AppConstants.AuthScheme;
    });


builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => false;
    options.MinimumSameSitePolicy = SameSiteMode.Lax;
});


builder.Services.AddHttpClient();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.UseCookiePolicy(); // required for cookies
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
