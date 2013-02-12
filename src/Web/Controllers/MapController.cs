using Core.Domain.Interfaces;
using Core.Domain.Model;
using Core.GeoJson;
using System.Web.Mvc;
using Web.Models.Map;

namespace Web.Controllers
{
	public class MapController : Controller
	{
		private IRepository _repository;
		public MapController(IRepository repository)
		{
			_repository = repository;
		}

		public ActionResult Display()
		{
			var model = new DisplayViewModel();
			return View(model);
		}

		[HttpPost]
		public ActionResult States()
		{
			var items = _repository.Find<State>();
			var fc = new FeatureCollection(items);
			return MaxJson(fc);
		}

		[HttpPost]
		public ActionResult Cities()
		{
			var items = _repository.Find<City>();
			var fc = new FeatureCollection(items);
			return MaxJson(fc);
		}

		private JsonResult MaxJson(object data)
		{
			var json = Json(data);
			json.MaxJsonLength = int.MaxValue;
			return json;
		}
	}
}
