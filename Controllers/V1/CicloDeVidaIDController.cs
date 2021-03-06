using System.Text;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ApiCatalogoJogos.Controllers.V1
{
    [Route("api/V1/[controller]")]
    [ApiController]
    public class CicloDeVidaIDController : ControllerBase
    {
        public readonly IExemploSingleton _exemploSingleton1;
        public readonly IExemploSingleton _exemploSingleton2;

        public readonly IExemploScoped _exemploScoped1;
        public readonly IExemploScoped _exemploScoped2;

        public readonly IExemploTransient _exemploTransient1;
        public readonly IExemploTransient _exemploTransient2;

        public CicloDeVidaIDController(IExemploSingleton exemploSingleton1,
                                       IExemploSingleton exemploSingleton2,
                                       IExemploScoped exemploScoped1,
                                       IExemploScoped exemploScoped2,
                                       IExemploTransient exemploTransient1,
                                       IExemploTransient exemploTransient2)
        {
            _exemploSingleton1 = exemploSingleton1;
            _exemploSingleton2 = exemploSingleton2;
            _exemploScoped1 = exemploScoped1;
            _exemploScoped2 = exemploScoped2;
            _exemploTransient1 = exemploTransient1;
            _exemploTransient2 = exemploTransient2;
        }

        [HttpGet]
        public Task<string> Get()
        {
            StringBuilder stringbuilder = new StringBuilder();

            stringbuilder.AppendLine($"Singleton 1: {_exemploSingleton1.Id}");
            stringbuilder.AppendLine($"Singleton 2: {_exemploSingleton2.Id}");
            stringbuilder.AppendLine();
            stringbuilder.AppendLine($"Scoped 1: {_exemploScoped1}");
            stringbuilder.AppendLine($"Scoped 2: {_exemploScoped2}");
            stringbuilder.AppendLine();
            stringbuilder.AppendLine($"Transient 1: {_exemploTransient1}");
            stringbuilder.AppendLine($"Transient 2: {_exemploTransient2}");

            return Task.FromResult(stringbuilder.ToString());
        }
    }
    public interface IExemploGeral
    {
        public Guid Id { get; }
    }
    public interface IExemploSingleton : IExemploGeral
    { }
    public interface IExemploScoped:IExemploGeral
    { }
    public interface IExemploTransient:IExemploGeral
    { }
    public class ExemploCicloDeVida : IExemploSingleton, IExemploScoped, IExemploTransient
    {
        private readonly Guid _guid;
        public ExemploCicloDeVida()
        {
            _guid = Guid.NewGuid();
        }
        public Guid Id => _guid;
    }
}
