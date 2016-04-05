using MundoCompilado.MinhaMeta.Domain;
using MundoCompilado.MinhaMeta.Model.Interfaces;
using System.Web.Http;

namespace MundoCompilado.MinhaMeta.Api.Controllers
{
    public abstract class ModelController<T, TList, TEdit> : ApiController
        where T : class, IModel
        where TEdit : class, IKey
    {
        protected IDomain<T> _domain;

        public ModelController(IDomain<T> domain)
        {
            _domain = domain;
        }

        [HttpGet]
        public virtual IHttpActionResult List()
        {
            return Ok(_domain.Get());
        }

        [HttpGet]
        public virtual IHttpActionResult Get(int id)
        {
            return Ok(_domain.Get(id));
        }

        [HttpPost]
        public virtual IHttpActionResult Post(TEdit model)
        {
            return Ok(_domain.Save(model));
        }

        [HttpDelete]
        public virtual IHttpActionResult Remove(TEdit model)
        {
            _domain.Remove(model.Id);
            return Ok();
        }
    }
}
