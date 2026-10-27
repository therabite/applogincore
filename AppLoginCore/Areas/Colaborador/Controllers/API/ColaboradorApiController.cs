using AppLoginCore.Models;
using AppLoginCore.Repository;
using AppLoginCore.Repository.Contract;
using Microsoft.AspNetCore.Mvc;

namespace AppLoginCore.Areas.Colaborador.Controllers.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class ColaboradorApiController : Controller
    {
        private readonly IColaboradorRepository _colaboradorRepository;

        public ColaboradorApiController(IColaboradorRepository colaboradorRepository)
        {
            _colaboradorRepository = colaboradorRepository;
        }
        [HttpGet]
        public IActionResult ListarCliente()
        {
            var colaborador = _colaboradorRepository.ObterTodosColaboradores();

            return Ok(colaborador);
        }
        [HttpPost]
        public IActionResult Post(Colaborador colaborador)
        {
            if (colaborador is null)
            {
                return BadRequest(new
                {
                    mensagem = "Os dados no cliente são obrigatórios"
                });
            }
            _colaboradorRepository.Cadastrar(colaborador);

            return Ok(new
            {
                mensagem = "Cadastro realizado com sucesso",
                colaborador = colaborador
            });
        }
        //GET api/ClienteApi/1
        [HttpGet("{id}")]

        public IActionResult BuscarPorId(int id)
        {
            var cliente = _colaboradorRepository.ObterColaborador(id);
            if (cliente is null)
            {
                return NotFound(new
                {
                    mensagem = "Colaborador não encontrado"
                });
            }
            return Ok(cliente);
        }

        [HttpPut("{id}")]

        public IActionResult Atualizar(int id, Colaborador colaborador)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var clienteExistente = _colaboradorRepository.ObterColaborador(id);

            if (clienteExistente == null)
            {
                return NotFound(new
                {
                    mensagem = "colaborador não encontrado"
                });
            }
            colaborador.Id = id;

            _colaboradorRepository.Atualizar(colaborador);

            return Ok(new
            {
                mensagem = "Colaborador atualizado com sucesso"
            });
        }

        [HttpPut("{id}")]

        public IActionResult Excluir(int id)
        {
            var colaborador = _colaboradorRepository.ObterColaborador(id);

            if (colaborador == null)
            {
                return NotFound(new
                {
                    mensagem = "Cliente nao encontrado"
                });
            }

            _colaboradorRepository.Excluir(id);

            return Ok(new
            {
                mensagem = "Cliente excluido com sucesso."
            });
        }
    }
}   
