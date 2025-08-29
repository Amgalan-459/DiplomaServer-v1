using Azure.Core;
using CourseProjectServer.Data.Context;
using CourseProjectServer.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;

namespace CourseProjectServer.Controllers { //потом добавить проверку по jwt токену, а потм и на posgtresql
    public class EntetiesController : Controller {
        private readonly ILogger<EntetiesController> _logger;
        private CourseDbContext dbContext;

        public EntetiesController (ILogger<EntetiesController> logger, CourseDbContext dbContext) {
            _logger = logger;
            this.dbContext = dbContext;
        }


        #region Get
        internal async Task<IEnumerable<Trainee>> GetAllTrainees () {
            _logger.LogInformation("trainee get all");
            
            return await dbContext.Trainees.ToArrayAsync();
        }

        internal async Task<IEnumerable<Trainer>> GetAllTrainers () {
            _logger.LogInformation("trainer get all");

            return await dbContext.Trainers.ToArrayAsync();
        }

        internal async Task<IEnumerable<Workout>> GetAllWorkouts () {
            _logger.LogInformation("workout get all");

            return await dbContext.Workouts.ToArrayAsync();
        }

        internal async Task<IEnumerable<ExerciseRaw>> GetAllExerciseRaws () {
            _logger.LogInformation("exercise raw get all");

            return await dbContext.ExerciseRaws.ToArrayAsync();
        }

        internal async Task<IEnumerable<Exercise>> GetAllExercises () {
            _logger.LogInformation("exercise get all");

            return await dbContext.Exercises.ToArrayAsync();
        }

        internal async Task<IEnumerable<Admin>> GetAllAdmins () {
            _logger.LogInformation("admin get all");

            return await dbContext.Admins.ToArrayAsync();
        }

        internal async Task<IEnumerable<KnowladgeBase>> GetAllKnowladgeBase () {
            _logger.LogInformation("knowladge base get all");

            return await dbContext.KnowladgeBases.ToArrayAsync();
        }

        internal async Task<Trainee> GetTrainee (int id) {
            _logger.LogInformation($"trainee get {id}");

            return await dbContext.Trainees.Where(t => t.Id == id).FirstOrDefaultAsync();
        }

        internal async Task<Trainer> GetTrainer (int id) {
            _logger.LogInformation($"trainer get {id}");

            return await dbContext.Trainers.Where(t => t.Id == id).FirstOrDefaultAsync();
        }

        internal async Task<Workout> GetWorkout (int id) {
            _logger.LogInformation($"workout get {id}");

            return await dbContext.Workouts.Where(t => t.Id == id).FirstOrDefaultAsync();
        }

        internal async Task<ExerciseRaw> GetExerciseRaw (int id) {
            _logger.LogInformation($"exercise raw get {id}");

            return await dbContext.ExerciseRaws.Where(t => t.Id == id).FirstOrDefaultAsync();
        }

        internal async Task<Exercise> GetExercise (int id) {
            _logger.LogInformation($"exercise get {id}");

            return await dbContext.Exercises.Where(t => t.Id == id).FirstOrDefaultAsync();
        }

        internal async Task<Admin> GetAdmin (int id) {
            _logger.LogInformation($"admin get {id}");

            return await dbContext.Admins.Where(t => t.Id == id).FirstOrDefaultAsync();
        }

        internal async Task<KnowladgeBase> GetKnowladgeBase (int id) {
            _logger.LogInformation($"knowladge base get {id}");

            return await dbContext.KnowladgeBases.Where(k => k.Id == id).FirstOrDefaultAsync();
        }

        internal async Task<IEnumerable<Workout>> GetWorkoutsByTraineeId (int id) {
            _logger.LogInformation($"workouts by trainee id: {id}");

            return await dbContext.Workouts.Where(w => w.TraineeId == id).ToArrayAsync();
        }

        internal async Task<IEnumerable<Exercise>> GetExerciseByWorkoutId (int id) {
            _logger.LogInformation($"exercise by trainee id: {id}");

            return await dbContext.Exercises.Where(e => e.WorkoutId == id).ToArrayAsync();
        }

        internal async Task<IEnumerable<Trainee>> GetTraineesByTrainerId (int id) {
            _logger.LogInformation($"trinee by trainer id: {id}");

            return await dbContext.Trainees.Where(t => t.TrainerId == id).ToArrayAsync();
        }

        internal async Task<Trainee?> GetTraineeByEmail (string email) {
            _logger.LogInformation($"trainee get by email {email}");

            return await dbContext.Trainees.FirstOrDefaultAsync(t => t.Email.Equals(email));
        }
        internal async Task<Trainer?> GetTrainerByEmail (string email) {
            _logger.LogInformation($"trainer get by email {email}");

            return await dbContext.Trainers.FirstOrDefaultAsync(t => t.Email.Equals(email));
        }
        #endregion


        #region Post
        internal async Task<IResult> AddTrainee (Trainee trainee) {
            _logger.LogInformation("trainee post");

            if (dbContext.Trainees.Contains(trainee)) {
                dbContext.Trainees.Update(trainee);
            }
            else {
                await dbContext.Trainees.AddAsync(trainee);
            }

            await dbContext.SaveChangesAsync();
            return TypedResults.Ok( trainee );
        }

        internal async Task<IResult> AddTrainer (Trainer trainer) {
            _logger.LogInformation("trainer post");

            if (dbContext.Trainers.Contains(trainer)) {
                dbContext.Trainers.Update(trainer);
            }
            else {
                await dbContext.Trainers.AddAsync(trainer);
            }

            await dbContext.SaveChangesAsync();
            return TypedResults.Ok( trainer );
        }

        internal async Task<IResult> AddWorkout (Workout workout) {
            _logger.LogInformation("workout post");

            if (dbContext.Workouts.Contains(workout)) {
                dbContext.Workouts.Update(workout);
            }
            else {
                await dbContext.Workouts.AddAsync(workout);
            }

            await dbContext.SaveChangesAsync();
            return TypedResults.Ok( workout );
        }

        internal async Task<IResult> AddExerciseRaw (ExerciseRaw exerciseRaw) {
            _logger.LogInformation("exercise raw post");

            if (dbContext.ExerciseRaws.Contains(exerciseRaw)) {
                dbContext.ExerciseRaws.Update(exerciseRaw);
            }
            else {
                await dbContext.ExerciseRaws.AddAsync(exerciseRaw);
            }

            await dbContext.SaveChangesAsync();
            return TypedResults.Ok(exerciseRaw);
        }

        internal async Task<IResult> AddExercise (Exercise exercise) {
            _logger.LogInformation("exercise post");

            if (dbContext.Exercises.Contains(exercise)) {
                dbContext.Exercises.Update(exercise);
            }
            else {
                await dbContext.Exercises.AddAsync(exercise);
            }

            await dbContext.SaveChangesAsync();
            return TypedResults.Ok(exercise);
        }

        internal async Task<IResult> AddAdmin (Admin admin) {
            _logger.LogInformation("admin post");

            if (dbContext.Admins.Contains(admin)) {
                dbContext.Admins.Update(admin);
            }
            else {
                await dbContext.Admins.AddAsync(admin);
            }

            await dbContext.SaveChangesAsync();
            return TypedResults.Ok(admin);
        }

        internal async Task<IResult> AddKnowladgeBase (KnowladgeBase knowladge) {
            _logger.LogInformation("knowladge base post");

            if (dbContext.KnowladgeBases.Contains(knowladge)) {
                dbContext.KnowladgeBases.Update(knowladge);
            }
            else {
                await dbContext.KnowladgeBases.AddAsync(knowladge);
            }

            await dbContext.SaveChangesAsync();
            return TypedResults.Ok(knowladge);
        }

        internal async Task<IResult> ForgotPasswordT(string email) {
            User? user = await dbContext.Trainees.FirstOrDefaultAsync(t => t.Email.Equals(email));

            if (user is null) {
                user = await dbContext.Trainers.FirstOrDefaultAsync(t => t.Email.Equals(email));
                if (user is null)
                {
                    _logger.LogInformation("user is not found");
                    return TypedResults.NotFound("Пользователь не найден");
                }                
            }

            int code = await SendCode(email);

            if (code == 0) {
                _logger.LogError("Unable to send mail");
                return TypedResults.Problem("email is not available");
            }

            _logger.LogInformation("mail sended");
            return TypedResults.Ok(code);
        }

        internal async Task<IResult> Login (UserDetails user) {
            User? findUser = dbContext.Trainees.FirstOrDefault(u => u.Email == user.Email && u.Password == user.Password);
            if (findUser is null) {
                return Results.Unauthorized();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, "User")
            };

            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                notBefore: now,
                claims: claims,
                expires: now.AddMinutes(AuthOptions.LIFETIME),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
            );

            var token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return Results.Ok(new AuthResponse {
                Token = token,
                Expiration = jwt.ValidTo
            });
        }
        #endregion


        #region Delete
        internal async Task<IResult> DeleteTrainee (int id) {
            _logger.LogInformation("trainee delete");
            Trainee? trainee = await dbContext.Trainees.Where(t => t.Id == id)
                .FirstOrDefaultAsync();
            if (trainee is null) {
                _logger.LogInformation("trainee not found");
                return TypedResults.NotFound($"Trainee с ID = {id} не найден");
            }
            dbContext.Trainees.Remove(trainee);

            await dbContext.SaveChangesAsync();
            return TypedResults.Ok();
        }

        internal async Task<IResult> DeleteTrainer (int id) {
            _logger.LogInformation("trainer delete");
            Trainer? trainer = await dbContext.Trainers.Where(t => t.Id == id)
                .FirstOrDefaultAsync();
            if (trainer is null) {
                _logger.LogInformation("trainer not found");
                return TypedResults.NotFound($"Trainer с ID = {id} не найден");
            }
            dbContext.Trainers.Remove(trainer);

            await dbContext.SaveChangesAsync();
            return TypedResults.Ok();
        }

        internal async Task<IResult> DeleteWorkout (int id) {
            _logger.LogInformation("workout delete");
            Workout? workout = await dbContext.Workouts.Where(w => w.Id == id)
                .FirstOrDefaultAsync();
            if (workout is null) {
                _logger.LogInformation("workout not found");
                return TypedResults.NotFound($"Workout с ID = {id} не найден");
            }
            dbContext.Workouts.Remove(workout);

            await dbContext.SaveChangesAsync();
            return TypedResults.Ok ();
        }

        internal async Task<IResult> DeleteExerciseRaw (int id) {
            _logger.LogInformation("exercise raw delete");
            ExerciseRaw? exerciseRaw = await dbContext.ExerciseRaws.Where(e => e.Id == id)
                .FirstOrDefaultAsync();
            if (exerciseRaw is null) {
                _logger.LogInformation("exercise raw not found");
                return TypedResults.NotFound($"Exercise raw с ID = {id} не найден");
            }
            dbContext.ExerciseRaws.Remove(exerciseRaw);

            await dbContext.SaveChangesAsync();
            return TypedResults.Ok();
        }

        internal async Task<IResult> DeleteExercise (int id) {
            _logger.LogInformation("exercise delete");
            Exercise? exercise = await dbContext.Exercises.Where(e => e.Id == id)
                .FirstOrDefaultAsync();
            if (exercise is null) {
                _logger.LogInformation("exercise not found");
                return TypedResults.NotFound($"Exercise с ID = {id} не найден");
            }
            dbContext.Exercises.Remove(exercise);

            await dbContext.SaveChangesAsync();
            return TypedResults.Ok();
        }

        internal async Task<IResult> DeleteAdmin (int id) {
            _logger.LogInformation("admin delete");
            Admin? admin = await dbContext.Admins.Where(a => a.Id == id)
                .FirstOrDefaultAsync();
            if (admin is null) {
                _logger.LogInformation("admin not found");
                return TypedResults.NotFound($"Admin с ID = {id} не найден");
            }
            dbContext.Admins.Remove(admin);

            await dbContext.SaveChangesAsync();
            return TypedResults.Ok();
        }

        internal async Task<IResult> DeleteKnowladgeBase (int id) {
            _logger.LogInformation("knowladge base delete");
            KnowladgeBase? knowladge = await dbContext.KnowladgeBases.Where(k =>  k.Id == id)
                .FirstOrDefaultAsync();
            if (knowladge is null) {
                _logger.LogInformation("knowladge base not found");
                return TypedResults.NotFound($"База знаний с ID = {id} не найдена");
            }
            dbContext.KnowladgeBases.Remove(knowladge);

            await dbContext.SaveChangesAsync();
            return TypedResults.Ok();
        }
        #endregion

        #region HelpMethods
        private async Task<int> SendCode (string email) {
            try {
                Random rnd = new();

                MailAddress from = new MailAddress("p_recovery@inbox.ru");
                MailAddress to = new MailAddress(email);

                MailMessage message = new(from, to);
                string code = "";
                for (int i = 0; i < 6; i++) {
                    code += rnd.Next(0, 9);
                }

                message.Subject = "Подтвердите вашу почту";

                message.IsBodyHtml = true;

                using SmtpClient smtpClient = new SmtpClient("smtp.mail.ru", 587);
                smtpClient.Credentials = new NetworkCredential("p_recovery@inbox.ru", "yBkdyDYRmrBd3Bzb3D5z");
                smtpClient.EnableSsl = true;
                message.Body = $@"<h1>Код: {code}</h1>";

                await smtpClient.SendMailAsync(message);

                return int.Parse(code);
            }
            catch(Exception ex) {
                _logger.LogError($"Unable to send message \n{ex}");
                return 0;
            }
        }
        #endregion
    }
}
