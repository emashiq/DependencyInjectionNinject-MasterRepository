using System.Web.Mvc;
using DependencyInjectionNinject.Models;
using DependencyInjectionNinject.Repository;

namespace DependencyInjection.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IUnitOfWork _uom;

        public MoviesController(IUnitOfWork uom)
        {
            _uom = uom;
        }
        // GET: Movies
        public ActionResult Index()
        {
            return View(_uom.Repository<Movie>().Get());
        }

        // GET: Movies/Details/5
        public ActionResult Details(int id)
        {
            Movie movie = _uom.Repository<Movie>().GetSingle(id: id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // GET: Movies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Title,ReleaseDate,Genre,Price")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                _uom.Repository<Movie>().Insert(movie);
                _uom.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(movie);
        }

        // GET: Movies/Edit/5
        public ActionResult Edit(int id)
        {
            Movie movie = _uom.Repository<Movie>().GetSingle(id: id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Title,ReleaseDate,Genre,Price")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                _uom.Repository<Movie>().Update(movie);
                _uom.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(movie);
        }

        // GET: Movies/Delete/5
        public ActionResult Delete(int id)
        {
            Movie movie = _uom.Repository<Movie>().GetSingle(id: id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Movie movie = _uom.Repository<Movie>().GetSingle(id: id);
            _uom.Repository<Movie>().Delete(id: id);
            _uom.SaveChanges();
            return RedirectToAction("Index");
        }

        
    }
}
