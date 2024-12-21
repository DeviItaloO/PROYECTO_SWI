using Microsoft.AspNetCore.Mvc;
using PROYECTO_SWI.Data;
using PROYECTO_SWI.Login;
using Microsoft.EntityFrameworkCore;
using PROYECTO_SWI.Models;

namespace PROYECTO_SWI.Controllers
{
    public class AccountController : Controller
    {
        private readonly PROYECTO_SWIContext _context;
        public AccountController(PROYECTO_SWIContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var usuario = await _context.Usuarios
                    .FirstOrDefaultAsync(u => u.Username == model.Username);

                if (usuario != null && VerifyPassword(model.Password, usuario.PasswordHash))
                {
                    //HttpContext.Session.SetInt32("PacienteId", usuario.IdPaciente);
                    HttpContext.Session.SetString("UserName", usuario.Username);
                    return RedirectToAction("Index", "Home");

                }
                else
                {
                    ModelState.AddModelError("", "Credenciales inválidas");
                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Registrar(RegistrarViewModel model)
        {

            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(model.Username))
                {
                    ModelState.AddModelError("", "El nombre de usuario no puede estar vacío.");
                    return View(model);
                }

                var existe = await _context.Usuarios
                    .FirstOrDefaultAsync(u => u.Username == model.Username);
                

                if (existe != null)
                {
                    ModelState.AddModelError("", "El nombre de usuario ya está en uso.");
                    return View(model);
                }

                if (model.Password != model.ConfirmPassword)
                {
                    ModelState.AddModelError("", "Las contraseñas no coinciden.");
                    return View(model);
                }

                var paciente = await _context.Pacientes
                    .FirstOrDefaultAsync(p => p.Nombre == model.Username);

                if(paciente == null)
                {
                    ModelState.AddModelError("", "No se encontró el paciente");
                    return View(model);
                }

                var usuario = new Usuario
                {
                    Username = model.Username,
                    PasswordHash = model.Password,
                    IdPaciente = paciente.IdPaciente
                };

                _context.Usuarios.Add(usuario);
                await _context.SaveChangesAsync();

                return RedirectToAction("Login", "Account");
            }

            return View(model);
        }

        private bool VerifyPassword(string password, string hashedPassword)
        {
            return password == hashedPassword;
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            Response.Headers["Cache-Control"] = "no-store";
            Response.Headers["Pragma"] = "no-cache";
            Response.Headers["Expires"] = "0";

            return RedirectToAction("Login", "Account");
        }
    }
}
