using HostrelHeadwaiterApp;
using HotelBusinessLogic.BusinessLogics;
using HotelBusinessLogic.OfficePackage;
using HotelBusinessLogic.OfficePackage.Implements;
using HotelContracts.BusinessLogicsContracts;
using HotelContracts.StoragesContracts;
using HotelDataBaseImplement.Implemets;
 
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<IReportHeadwaiterLogic, ReportLogicHeadwaiter>();
builder.Services.AddTransient<ILunchStorage, LunchStorage>();
builder.Services.AddTransient<IMealPlanStorage, MealPlanStorage>();
builder.Services.AddTransient<IConferenceBookingStorage, ConferenceBookingStorage>();
builder.Services.AddTransient<IConferenceStorage, ConferenceStorage>();
builder.Services.AddTransient<IRoomStorage, RoomStorage>();
builder.Services.AddTransient<AbstractSaveToExcelHeadwaiter, SaveToExcelHeadwaiter>();
builder.Services.AddTransient<AbstractSaveToPdfHeadwaiter, SaveToPdfHeadwaiter>();
builder.Services.AddTransient<AbstractSaveToWordHeadwaitre, SaveToWordHeadwaiter>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();
APIClient.Connect(builder.Configuration);

// Configure the HTTP request pipeline.
app.UseExceptionHandler("/Home/Error");
// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
app.UseHsts();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
