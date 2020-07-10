using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using FichaAcademia.Dominio.Models;
using Microsoft.AspNetCore.Authorization;
using FichaAcademia.AcessoDados.Interfaces;

namespace FichaAcademia.Controllers
{
    [Authorize]
    public class ExerciciosController : Controller
    {
        private readonly IExercicioRepositorio _exercicioRepositorio;
        private readonly ICategoriaExercicioRepositorio _categoriaExercicioRepositorio;
        private readonly IListaExercicioRepositorio _listaExercicioRepositorio;

        public ExerciciosController(IExercicioRepositorio exercicioRepositorio, ICategoriaExercicioRepositorio categoriaExercicioRepositorio, IListaExercicioRepositorio listaExercicioRepositorio)
        {
            _exercicioRepositorio = exercicioRepositorio;
            _categoriaExercicioRepositorio = categoriaExercicioRepositorio;
            _listaExercicioRepositorio = listaExercicioRepositorio;
        }

        // GET: Exercicios
        public async Task<IActionResult> Index()
        {
            return View(await _exercicioRepositorio.PegarTodos());
        }

        public async Task<IActionResult> Listagem(int FichaId, int AlunoId)
        {
            ViewData["FichaId"] = FichaId;
            ViewData["AlunoId"] = AlunoId;

            return View(await _exercicioRepositorio.PegarTodos());
        }


        public async Task<IActionResult> AdicionarExercicio(int exercicioId, int frequencia, int repeticoes, int carga, int fichaId)
        {
            if (await _listaExercicioRepositorio.ExercicioExisteNaFicha(exercicioId, fichaId))
                return Json(false);

            ListaExercicio listaExercicio = new ListaExercicio
            {
                ExercicioId = exercicioId,
                Frequencia = frequencia,
                Repeticoes = repeticoes,
                Carga = carga,
                FichaId = fichaId
            };

            if (ModelState.IsValid)
            {
                await _listaExercicioRepositorio.Inserir(listaExercicio);
                return Json(true);
            }
            else return Json(false);
        }

        public async Task<IActionResult> Create()
        {
            ViewData["CategoriaExercicioId"] = new SelectList(_categoriaExercicioRepositorio.PegarTodos(), "CategoriaExercicioId", "Nome");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ExercicioId,Nome,CategoriaExercicioId")] Exercicio exercicio)
        {
            if (ModelState.IsValid)
            {
                await _exercicioRepositorio.Inserir(exercicio);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriaExercicioId"] = new SelectList(_categoriaExercicioRepositorio.PegarTodos(), "CategoriaExercicioId", "Nome", exercicio.CategoriaExercicioId);
            return View(exercicio);
        }


        public async Task<IActionResult> Edit(int id)
        {     
            var exercicio = await _exercicioRepositorio.PegarPeloId(id);
            if (exercicio == null)
            {
                return NotFound();
            }
            ViewData["CategoriaExercicioId"] = new SelectList(_categoriaExercicioRepositorio.PegarTodos(), "CategoriaExercicioId", "Nome", exercicio.CategoriaExercicioId);
            return View(exercicio);
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ExercicioId,Nome,CategoriaExercicioId")] Exercicio exercicio)
        {
            if (id != exercicio.ExercicioId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {   
                await _exercicioRepositorio.Atualizar(exercicio);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriaExercicioId"] = new SelectList(_categoriaExercicioRepositorio.PegarTodos(), "CategoriaExercicioId", "Nome", exercicio.CategoriaExercicioId);
            return View(exercicio);
        }  
       
        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            await _exercicioRepositorio.Excluir(id);
            return Json("Exercício excluído com sucesso");
        }

        public async Task<JsonResult> ExercicioExiste(string nome, int ExercicioId)
        {
            if(ExercicioId == 0)
            {
                if (await _exercicioRepositorio.ExecicioExiste(nome))
                    return Json("Exercício já existe");

                return Json(true);
            }
            else
            {
                if(await _exercicioRepositorio.ExecicioExiste(nome, ExercicioId))
                    return Json("Exercício já existe");

                return Json(true);
            }
        }
    }
}
