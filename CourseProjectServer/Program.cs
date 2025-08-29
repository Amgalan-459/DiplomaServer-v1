
using CourseProjectServer.Controllers;
using CourseProjectServer.Data.Context;
using CourseProjectServer.Data.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace CourseProjectServer {
    public class Program {
        public static async Task Main (string[] args) {       
            await CreateHostBuiler(args).Build().RunAsync();
        }

        public static IHostBuilder CreateHostBuiler (string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => {
                    webBuilder.UseKestrel();
                    webBuilder.UseUrls("http://localhost:5000/");
                    webBuilder.UseStartup<Startup>();
                });
    }

    public class Startup {
        public void ConfigureServices (IServiceCollection services) {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddScoped<EntetiesController>();
            services.AddDbContext<CourseDbContext>();

            services.AddCors(options =>
            {
                options.AddPolicy("DevCors", builder =>
                {
                    builder
                        .WithOrigins("http://localhost:4200") // твой Angular
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });

                options.AddPolicy("ProdCors", builder =>
                {
                    builder
                        .WithOrigins("https://myapp.com") // домен продакшн фронтенда
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });


            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters {
                        // укзывает, будет ли валидироваться издатель при валидации токена
                        ValidateIssuer = true,
                        // строка, представляющая издателя
                        ValidIssuer = AuthOptions.ISSUER,

                        // будет ли валидироваться потребитель токена
                        ValidateAudience = true,
                        // установка потребителя токена
                        ValidAudience = AuthOptions.AUDIENCE,
                        // будет ли валидироваться время существования
                        ValidateLifetime = true,

                        // установка ключа безопасности
                        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                        // валидация ключа безопасности
                        ValidateIssuerSigningKey = true,
                    };
                });
        }

        public void Configure (IApplicationBuilder app, IWebHostEnvironment env) {
            app.UseRouting();

            if (env.IsDevelopment()) {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseCors("DevCors");
            }
            else {
                app.UseCors("ProdCors");
            }

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => {

                #region MapGet
                endpoints.MapGet("/api/trainee/all", [Authorize] (EntetiesController controller) =>
                    controller.GetAllTrainees());
                endpoints.MapGet("/api/trainee/{id:int}", (EntetiesController controller, int id) =>
                    controller.GetTrainee(id));
                endpoints.MapGet("/api/trainee/{email}", (EntetiesController controller, string email) =>
                    controller.GetTraineeByEmail(email));
                endpoints.MapGet("/api/trainee/trainer/{id:int}", (EntetiesController controller, int id) =>
                    controller.GetTraineesByTrainerId(id));

                //потом смотрим от кого. Если от тренера или админа - пароль надо, а если нет, то нет
                endpoints.MapGet("/api/trainer/all", (EntetiesController controller) =>
                    controller.GetAllTrainers());
                endpoints.MapGet("/api/trainer/{id:int}", (EntetiesController controller, int id) =>
                    controller.GetTrainer(id));
                endpoints.MapGet("/api/trainer/{email}", (EntetiesController controller, string email) =>
                    controller.GetTrainerByEmail(email));

                endpoints.MapGet("/api/workout/all", (EntetiesController controller) =>
                    controller.GetAllWorkouts());
                endpoints.MapGet("/api/workout/{id:int}", (EntetiesController controller, int id) =>
                    controller.GetWorkout(id));
                endpoints.MapGet("/api/workout/trainee/{id:int}", (EntetiesController controller, int id) =>
                    controller.GetWorkoutsByTraineeId(id));

                endpoints.MapGet("/api/exercise/all", (EntetiesController controller) =>
                    controller.GetAllExercises());
                endpoints.MapGet("/api/exercise/{id:int}", (EntetiesController controller, int id) =>
                    controller.GetExercise(id));
                endpoints.MapGet("/api/exercise/workout/{id:int}", (EntetiesController controller, int id) =>
                    controller.GetExerciseByWorkoutId(id));

                endpoints.MapGet("/api/exerciseraw/all", (EntetiesController controller) =>
                    controller.GetAllExerciseRaws());
                endpoints.MapGet("/api/exerciseraw/{id:int}", (EntetiesController controller, int id) =>
                    controller.GetExerciseRaw(id));

                endpoints.MapGet("/api/admin/all", (EntetiesController controller) =>
                    controller.GetAllAdmins());
                endpoints.MapGet("/api/admin/{id:int}", (EntetiesController controller, int id) =>
                    controller.GetAdmin(id));

                endpoints.MapGet("/api/knowladgebase/all", (EntetiesController controller) =>
                    controller.GetAllKnowladgeBase());
                endpoints.MapGet("/api/knowladgebase/{id:int}", (EntetiesController controller, int id) =>
                    controller.GetKnowladgeBase(id));
                #endregion


                #region MapPost
                endpoints.MapPost("/api/trainee",
                    (EntetiesController controller, [FromBody] Trainee trainee) =>
                        controller.AddTrainee(trainee));

                endpoints.MapPost("/api/trainer",
                    [Authorize] (EntetiesController controller, [FromBody] Trainer trainer) =>
                        controller.AddTrainer(trainer));

                endpoints.MapPost("/api/workout",
                    [Authorize] (EntetiesController controller, [FromBody] Workout workout) =>
                        controller.AddWorkout(workout));

                endpoints.MapPost("/api/exercise",
                    [Authorize] (EntetiesController controller, [FromBody] Exercise exercise) =>
                        controller.AddExercise(exercise));

                endpoints.MapPost("/api/exerciseraw",
                    [Authorize] (EntetiesController controller, [FromBody] ExerciseRaw exerciseRaw) =>
                        controller.AddExerciseRaw(exerciseRaw));

                endpoints.MapPost("/api/admin",
                    [Authorize] (EntetiesController controller, [FromBody] Admin admin) =>
                        controller.AddAdmin(admin));

                endpoints.MapPost("/api/knowladgebase",
                    [Authorize] (EntetiesController controller, [FromBody] KnowladgeBase knowladge) =>
                        controller.AddKnowladgeBase(knowladge));
                
                //также для подтверждения email. Еще потом добавить в бд isEmailApproved
                endpoints.MapPost("/api/forgetpassword/{email}",
                    (EntetiesController controller, string email) =>
                        controller.ForgotPasswordT(email));

                endpoints.MapPost("/api/user/login", (EntetiesController controller, [FromBody] UserDetails user) =>
                    controller.Login(user));

                //endpoints.MapPost("/api/user/exercises")

                /*
                 * "/api/user/"
                 * "/api/user/"
                 * "/api/user/"
                 * "/api/user/"
                 * "/api/user/"
                 * "/api/user/"
                 * "/api/user/"
                 * "/api/user/"
                 */
                #endregion


                //delete не пользуемся, а меняем в юзере или админе настройку IsActive. Delete в основном только к exercise
                #region MapDelete
                endpoints.MapDelete("/api/trainee/delete/{id:int}",
                    (EntetiesController controller, int id) => controller.DeleteTrainee(id));

                endpoints.MapDelete("/api/trainer/delete/{id:int}",
                    (EntetiesController controller, int id) => controller.DeleteTrainer(id));

                endpoints.MapDelete("/api/workout/delete/{id:int}",
                    (EntetiesController controller, int id) => controller.DeleteWorkout(id));

                endpoints.MapDelete("/api/exercise/delete/{id:int}",
                    (EntetiesController controller, int id) => controller.DeleteExercise(id));

                endpoints.MapDelete("/api/exerciseraw/delete/{id:int}",
                    (EntetiesController controller, int id) => controller.DeleteExerciseRaw(id));

                endpoints.MapDelete("/api/admin/{id:int}",
                    (EntetiesController controller, int id) => controller.DeleteAdmin(id));

                endpoints.MapDelete("/api/admin/delete/{id:int}",
                    (EntetiesController controller, int id) => controller.DeleteKnowladgeBase(id));
                #endregion
            });
        }
    }
}
