using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MIS.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace MIS.Controllers
{
    public class HomeController : Controller
    {
        [Authorize(Policy = "a")]
        [HttpGet("Index")]
        public IActionResult Home()
        {
            return View();
        }

        [Authorize(Policy = "a")]
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AccessDenied()
        {
            return View();
        }

        // ... other actions ...

        private readonly ApplicationDb _dbContext;

        public HomeController(ApplicationDb dbContext)
        {
            _dbContext = dbContext;
        }


        [HttpGet("Quiz")]
        public IActionResult Quiz(int questionIndex = 0)
        {
            List<Question> quizQuestions = _dbContext.Questions.ToList();

            if (questionIndex >= 0 && questionIndex < quizQuestions.Count)
            {
                return View(quizQuestions);
            }

            return RedirectToAction("Result");
        }



        [HttpGet("Result")]
        public IActionResult Result()
        {
            return View();
        }

        [HttpGet("AddQuestion")]
        public IActionResult AddQuestion()
        {
            return View();
        }

        [HttpPost("AddQuestion")]
        public IActionResult AddQuestion(Question question)
        {
            _dbContext.Questions.Add(question);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost("SubmitAnswer")]
        public IActionResult SubmitAnswer(List<Answer> answers)
        {
            // Save user's answers (implement database storage)

            int score = CalculateScore(answers);
            ViewBag.Score = score;

            return View("Result");
        }

        private int CalculateScore(List<Answer> userAnswers)
        {
            List<Question> correctAnswers = _dbContext.Questions.ToList();
            int score = 0;

            for (int i = 0; i < userAnswers.Count; i++)
            {
                if (userAnswers[i].SelectedOptionIndex == correctAnswers[i].CorrectAnswerIndex)
                {
                    score++;
                }
            }

            return score;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
