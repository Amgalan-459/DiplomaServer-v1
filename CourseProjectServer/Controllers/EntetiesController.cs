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

        internal async Task<IEnumerable<Course>> GetAllCourses () {
            _logger.LogInformation("course get all");

            return await dbContext.Courses.ToArrayAsync();
        }

        internal async Task<IEnumerable<Course>> GetAllAvailableCourses () {
            _logger.LogInformation("course available get all");

            return await dbContext.Courses.Where(c => c.IsAvaibale).ToArrayAsync();
        }

        internal async Task<IEnumerable<Module>> GetAllModules () {
            _logger.LogInformation("module get all");

            return await dbContext.Modules.ToArrayAsync();
        }

        internal async Task<IEnumerable<Module>> GetAllAvailableModules () {
            _logger.LogInformation("module available get all");

            return await dbContext.Modules.Where(m => m.IsAvailable).ToArrayAsync();
        }

        internal async Task<IEnumerable<Lesson>> GetAllLessons () {
            _logger.LogInformation("lesson get all");

            return await dbContext.Lessons.ToArrayAsync();
        }

        internal async Task<IEnumerable<Lesson>> GetAllAvailableLessons () {
            _logger.LogInformation("lesson available get all");

            return await dbContext.Lessons.Where(l => l.IsAvailable).ToArrayAsync();
        }

        internal async Task<IEnumerable<Test>> GetAllTests () {
            _logger.LogInformation("test get all");

            return await dbContext.Tests.ToArrayAsync();
        }

        internal async Task<IEnumerable<Test>> GetAllAvailableTests () {
            _logger.LogInformation("test available get all");

            return await dbContext.Tests.Where(t => t.IsAvailable).ToArrayAsync();
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

        internal async Task<Course> GetCourse (int id) {
            _logger.LogInformation($"course base get {id}");

            return await dbContext.Courses.Where(c => c.Id == id).FirstOrDefaultAsync();
        }

        //потом переделать на права, чтобы админ мог все, а юзер только доступное и чтобы все было в 1 функции
        internal async Task<Course> GetAvailableCourse (int id) {
            _logger.LogInformation($"course available base get {id}");

            return await dbContext.Courses.Where(c => c.Id == id && c.IsAvaibale).FirstOrDefaultAsync();
        }

        internal async Task<Module> GetModule (int id) {
            _logger.LogInformation($"module base get {id}");

            return await dbContext.Modules.Where(m => m.Id == id).FirstOrDefaultAsync();
        }

        internal async Task<Module> GetAvailableModule (int id) {
            _logger.LogInformation($"module available base get {id}");

            return await dbContext.Modules.Where(m => m.Id == id && m.IsAvailable).FirstOrDefaultAsync();
        }

        internal async Task<Lesson> GetLesson (int id) {
            _logger.LogInformation($"lesson base get {id}");

            return await dbContext.Lessons.Where(l => l.Id == id).FirstOrDefaultAsync();
        }

        internal async Task<Lesson> GetAvailableLesson (int id) {
            _logger.LogInformation($"lesson available base get {id}");

            return await dbContext.Lessons.Where(l => l.Id == id && l.IsAvailable).FirstOrDefaultAsync();
        }

        internal async Task<Test> GetTest (int id) {
            _logger.LogInformation($"test base get {id}");

            return await dbContext.Tests.Where(t => t.Id == id).FirstOrDefaultAsync();
        }

        internal async Task<Test> GetAvailableTest (int id) {
            _logger.LogInformation($"test available base get {id}");

            return await dbContext.Tests.Where(t => t.Id == id && t.IsAvailable).FirstOrDefaultAsync();
        }

        internal async Task<IEnumerable<Workout>> GetWorkoutsByTraineeId (int id) {
            _logger.LogInformation($"workouts by trainee id: {id}");

            return await dbContext.Workouts.Where(w => w.TraineeId == id).ToArrayAsync();
        }

        internal async Task<IEnumerable<Exercise>> GetExerciseByWorkoutId (int id) {
            _logger.LogInformation($"exercise by workout id: {id}");

            return await dbContext.Exercises.Where(e => e.WorkoutId == id).ToArrayAsync();
        }

        internal async Task<IEnumerable<Trainee>> GetTraineesByTrainerId (int id) {
            _logger.LogInformation($"trinee by trainer id: {id}");

            return await dbContext.Trainees.Where(t => t.TrainerId == id).ToArrayAsync();
        }

        internal async Task<IEnumerable<Module>> GetModulesByCourseId (int id) {
            _logger.LogInformation($"module by course id: {id}");

            return await dbContext.Modules.Where(m => m.CourseId == id).ToArrayAsync();
        }

        internal async Task<IEnumerable<Module>> GetAvailableModulesByCourseId (int id) {
            _logger.LogInformation($"module available by course id: {id}");

            return await dbContext.Modules.Where(m => m.CourseId == id && m.IsAvailable).ToArrayAsync();
        }

        internal async Task<IEnumerable<Course>> GetCoursesByUserId (int id) {
            _logger.LogInformation($"course by user id: {id}");

            return await dbContext.Courses.Where(c => c.UserId == id).ToArrayAsync();
        }

        internal async Task<IEnumerable<Lesson>> GetLessonsByModuleId (int id) {
            _logger.LogInformation($"lesson by module id: {id}");

            return await dbContext.Lessons.Where(l => l.ModuleId == id).ToArrayAsync();
        }

        internal async Task<IEnumerable<Lesson>> GetAvailableLessonsByModuleId (int id) {
            _logger.LogInformation($"lesson available by module id: {id}");

            return await dbContext.Lessons.Where(l => l.ModuleId == id && l.IsAvailable).ToArrayAsync();
        }

        internal async Task<IEnumerable<Test>> GetTestsByModuleId (int id) {
            _logger.LogInformation($"module by course id: {id}");

            return await dbContext.Tests.Where(t => t.ModuleId == id).ToArrayAsync();
        }

        internal async Task<IEnumerable<Test>> GetAvailableTestsByModuleId (int id) {
            _logger.LogInformation($"module available by course id: {id}");

            return await dbContext.Tests.Where(t => t.ModuleId == id && t.IsAvailable).ToArrayAsync();
        }

        internal async Task<IEnumerable<KnowladgeBase>> GetKnowledgeBasesByTraineeId (int traineeId) {
            _logger.LogInformation($"knowledge base by trainee id: {traineeId}");

            TraineesKnowledgeBases[] traineesKnowledgeBases = await dbContext.TraineesKnowledgeBasess
                .Where(tK => tK.TraineeId == traineeId)
                .ToArrayAsync();

            IList<KnowladgeBase> knowladgeBases = new List<KnowladgeBase>();
            KnowladgeBase[] allKnoledge = await dbContext.KnowladgeBases.ToArrayAsync();
            foreach(var traineesKnowledgeBase in traineesKnowledgeBases) {
                foreach(var knowledge in allKnoledge) {
                    if (knowledge.Id == traineesKnowledgeBase.KnowledgeId) {
                        knowladgeBases.Add(knowledge);
                    }
                }
            }

            return knowladgeBases.ToArray();
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

        internal async Task<IResult> AddCourse (Course course) {
            //проверка, что такого имени нет у других курсов
            _logger.LogInformation("course post");

            if (dbContext.Courses.Contains(course)) {
                dbContext.Courses.Update(course);
            }
            else {
                await dbContext.Courses.AddAsync(course);
            }

            await dbContext.SaveChangesAsync();
            return TypedResults.Ok(course);
        }

        internal async Task<IResult> AddModule (Module module) {
            _logger.LogInformation("module post");

            if (dbContext.Modules.Contains(module)) {
                dbContext.Modules.Update(module);
            }
            else {
                await dbContext.Modules.AddAsync(module);
            }

            await dbContext.SaveChangesAsync();
            return TypedResults.Ok(module);
        }

        internal async Task<IResult> AddLesson (Lesson lesson) {
            _logger.LogInformation("lesson post");

            if (dbContext.Lessons.Contains(lesson)) {
                dbContext.Lessons.Update(lesson);
            }
            else {
                await dbContext.Lessons.AddAsync(lesson);
            }

            await dbContext.SaveChangesAsync();
            return TypedResults.Ok(lesson);
        }

        internal async Task<IResult> AddTest (Test test) {
            _logger.LogInformation("test post");

            if (dbContext.Tests.Contains(test)) {
                dbContext.Tests.Update(test);
            }
            else {
                await dbContext.Tests.AddAsync(test);
            }

            await dbContext.SaveChangesAsync();
            return TypedResults.Ok(test);
        }

        internal async Task<IResult> AddTraineesKnowledgeBases (int traineeId, int knowledgeId) {
            _logger.LogInformation("TraineesKnowledgeBases post");
            TraineesKnowledgeBases traineesKnowledgeBases = new TraineesKnowledgeBases(traineeId, knowledgeId);

            if (dbContext.TraineesKnowledgeBasess.Contains(traineesKnowledgeBases)) {
                dbContext.TraineesKnowledgeBasess.Update(traineesKnowledgeBases);
            }
            else {
                await dbContext.TraineesKnowledgeBasess.AddAsync(traineesKnowledgeBases);
            }

            await dbContext.SaveChangesAsync();
            return TypedResults.Ok("Добавлено");
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

        internal async Task<IResult> PurshuaseCourse (int userId, Course course) {
            if (course.UserId == userId) {
                return TypedResults.BadRequest("У пользователя уже есть данный курс");
            }

            await dbContext.Courses.AddAsync(new Course(course.Title, course.Author, course.Raiting, course.ProgressText, course.Type,
                course.Image, true, course.Instructions, course.IsAvaibale, course.TrainerId, userId));
            await dbContext.SaveChangesAsync();

            Module[] modules = await dbContext.Modules.Where(m => m.CourseId == course.Id).ToArrayAsync();

            if (modules.Count() == 0) {
                return TypedResults.NotFound("Не нашел подобный курс");
            }

            Course? foundedCourse = dbContext.Courses.Where(c => c.Equals(course)).FirstOrDefault();
            if (foundedCourse != null) {
                return TypedResults.BadRequest("Ошибка в добавлении курса");
            }

            foreach (var module in modules) {
                Lesson[] lessons = await dbContext.Lessons.Where(l => l.ModuleId == module.Id).ToArrayAsync();
                Test[] tests = await dbContext.Tests.Where(t => t.ModuleId == module.Id).ToArrayAsync();

                if (lessons.Count() == 0 || tests.Count() == 0) {
                    return TypedResults.NotFound("Не нашел тесты или уроки по данному модулю");
                }

                Module newModule = new(module.Title, true, module.IsAvailable, foundedCourse!.Id);
                await dbContext.Modules.AddAsync(newModule);
                await dbContext.SaveChangesAsync();
                Module? foundedModule = dbContext.Modules.Where(m => m.Equals(newModule))
                    .FirstOrDefault();
                if (foundedModule != null) {
                    return TypedResults.NotFound("произошла ошибка в добавлении нового модуля");
                }

                foreach(Lesson lesson in lessons) {
                    await dbContext.Lessons.AddAsync(new Lesson(lesson.Title, false, lesson.VideoUrl,
                        lesson.Content, lesson.IsAvailable, foundedModule!.Id));
                }
                foreach (Test test in tests) {
                    await dbContext.Tests.AddAsync(new Test(test.Title, false, test.Url, test.IsAvailable, foundedModule!.Id));
                }
                await dbContext.SaveChangesAsync();
            }

            await dbContext.SaveChangesAsync();
            return TypedResults.Ok(course);
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

        internal async Task<IResult> DeleteCourse (int id) {
            _logger.LogInformation("course base delete");
            Course? course = await dbContext.Courses.Where(c => c.Id == id)
                .FirstOrDefaultAsync();
            if (course is null) {
                _logger.LogInformation("course base not found");
                return TypedResults.NotFound($"Курс с ID = {id} не найдена");
            }
            dbContext.Courses.Remove(course);

            await dbContext.SaveChangesAsync();
            return TypedResults.Ok();
        }

        internal async Task<IResult> DeleteModule (int id) {
            _logger.LogInformation("module base delete");
            Module? module = await dbContext.Modules.Where(m => m.Id == id)
                .FirstOrDefaultAsync();
            if (module is null) {
                _logger.LogInformation("module base not found");
                return TypedResults.NotFound($"Модуль с ID = {id} не найдена");
            }
            dbContext.Modules.Remove(module);

            await dbContext.SaveChangesAsync();
            return TypedResults.Ok();
        }

        internal async Task<IResult> DeleteLesson (int id) {
            _logger.LogInformation("lesson base delete");
            Lesson? lesson = await dbContext.Lessons.Where(l => l.Id == id)
                .FirstOrDefaultAsync();
            if (lesson is null) {
                _logger.LogInformation("lesson base not found");
                return TypedResults.NotFound($"Урок с ID = {id} не найдена");
            }
            dbContext.Lessons.Remove(lesson);

            await dbContext.SaveChangesAsync();
            return TypedResults.Ok();
        }

        internal async Task<IResult> DeleteTest (int id) {
            _logger.LogInformation("test base delete");
            Test? test = await dbContext.Tests.Where(t => t.Id == id)
                .FirstOrDefaultAsync();
            if (test is null) {
                _logger.LogInformation("test base not found");
                return TypedResults.NotFound($"Тест с ID = {id} не найдена");
            }
            dbContext.Tests.Remove(test);

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
