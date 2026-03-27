using ASP;
using ASP.Models;

var builder = WebApplication.CreateBuilder(args);

//Dependency Injection
//Services in einem DI Container registrieren, und in weiterer Folge im Programmumfeld verwenden
//Im MVC Bereich werden diese Objekte im Controller empfangen (Konstruktor)

// Add services to the container.
builder.Services.AddControllersWithViews();

//Drei Methoden:
//- AddSingleton: Ein Objekt für alle User (wird niemals neu erstellt)
//- AddTransient: Ein Objekt pro User (ein User bekommt das Objekt, und verwendet dieses für jede Page)
//- AddScoped: Ein Objekt pro Request (Objekt wird bei Anforderung immer neu erstellt)
//WICHTIG: Alle Add-Methoden bauen auf AddSingleton/Transient/Scoped auf
builder.Services.AddSingleton<CounterService>();
builder.Services.AddSingleton<ICounterService, CounterService>();
//builder.Services.AddKeyedSingleton<CounterService>("CS"); //Hier kann dem Service ein Name verliehen werden

//DB Verbindung hinzufügen (per Dependency Injection)
string? connStr = builder.Configuration.GetConnectionString("ConnString");
if (connStr != null)
	builder.Services.AddSqlServer<NorthwindContext>(connStr, optionsAction: o =>
	{
		//Mithilfe von Options kann die Add-Methode konfiguriert werden
		if (builder.Environment.IsDevelopment())
		{
			o.EnableSensitiveDataLogging();
			o.EnableDetailedErrors();
		}
	});

var app = builder.Build();

////////////////////////////////////////////////////////////////////////

//Middleware
//HTTP-Request Pipeline konfigurieren
//Wenn ein User die Webseite angreift, wird diese Pipeline durchgegangen
//z.B.: User auf die korrekte Sprache weiterleiten

if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler();
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}")
	.WithStaticAssets();


app.Run();
