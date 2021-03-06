﻿using ApiCatalogoJogos.Entities;
using ApiCatalogoJogos.Exceptions;
using ApiCatalogoJogos.ImputModel;
using ApiCatalogoJogos.Repositories;
using ApiCatalogoJogos.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoJogos.Services
{
    public class JogoService: IJogoService
    {
        private readonly IJogoRepository _jogoRepository;

        public JogoService(IJogoRepository JogoRepository)
        {
            _jogoRepository = JogoRepository;
        }
        public async Task<List<JogoViewModel>> Obter(int pagina, int quantidade)
        {
            var jogos = await _jogoRepository.Obter(pagina, quantidade);
            return jogos.Select(jogo => new JogoViewModel
            {
                Id = jogo.Id,
                Nome = jogo.Nome,
                Produtora = jogo.Produtora,
                Preco = jogo.Preco
            }).ToList();
        }
        public async Task<JogoViewModel> Obter(Guid id)
        {
            var jogo = await _jogoRepository.Obter(id);
            if (jogo == null)
                return null;
            return new JogoViewModel
            {
                Id = jogo.Id,
                Nome = jogo.Nome,
                Produtora = jogo.Produtora,
                Preco = jogo.Preco
            };

        }

        public async Task<JogoViewModel> Inserir(JogoImputModel jogo)
        {
            var entidadeJogo = await _jogoRepository.Obter(jogo.Nome, jogo.Produtora);

            if (entidadeJogo.Count > 0)
                throw new JogoJaCadastradoException();

            var jogoinsert = new Jogo
            {
                Id = Guid.NewGuid(),
                Nome = jogo.Nome,
                Produtora = jogo.Produtora,
                Preco = jogo.Preco
            };

            await _jogoRepository.Inserir(jogoinsert);

            return new JogoViewModel
            {
                Id = jogoinsert.Id,
                Nome = jogoinsert.Nome,
                Produtora = jogoinsert.Produtora,
                Preco = jogoinsert.Preco
            };
        }

        public async Task Atualizar (Guid id, JogoImputModel jogo)
        {
            var entidadejogo = await _jogoRepository.Obter(id);

            if (entidadejogo == null)
                throw new JogoNaoCadastradoException();
            entidadejogo.Nome = jogo.Nome;
            entidadejogo.Produtora = jogo.Produtora;
            entidadejogo.Preco = jogo.Preco;

            await _jogoRepository.Atualizar(entidadejogo);
        }

        public async Task Atualizar (Guid id, double preco)
        {
            var entidadejogo = await _jogoRepository.Obter(id);

            if (entidadejogo == null)
                throw new JogoNaoCadastradoException();

            entidadejogo.Preco = preco;

            await _jogoRepository.Atualizar(entidadejogo);
        }

        public async Task Remover(Guid id)
        {
            var jogo = await _jogoRepository.Obter(id);
            if (jogo == null)
                throw new JogoNaoCadastradoException();
            await _jogoRepository.Remover(id);
        }

        public void Dispose()
        {
            _jogoRepository?.Dispose();
        }
     
    }
}
