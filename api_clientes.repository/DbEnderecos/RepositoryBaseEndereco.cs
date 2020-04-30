﻿using api_clientes.data;
using api_clientes.domain.core.Abstracts.Repositories.DbEnderecos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace api_clientes.repository.DbEnderecos
{
    public class RepositoryBaseEndereco<TEntity> : IRepositoryBaseDbEnderecos<TEntity>, IDisposable where TEntity : class
    {
        #region Construtor
        private readonly EnderecosContext context;

        public RepositoryBaseEndereco(EnderecosContext context)
        {
            this.context = context;
        }
        #endregion

        #region Dispose
        public void Dispose()
        {
            context.Dispose();
        }
        #endregion

        #region Listar
        /// <summary>
        /// Listar
        /// </summary>
        /// <returns></returns>
        public virtual List<TEntity> Listar()
        {
            var entities = from x in context.Set<TEntity>()
                           .OrderBy(x => (true))
                           select x;

            return entities.ToList();
        }
        #endregion

        #region Obter
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual TEntity Obter(int? id)
        {
            var entity = context.Set<TEntity>().Find(id);

            return entity;
        }
        #endregion

        #region Inserir
        /// <summary>
        /// Inserir
        /// </summary>
        /// <param name="entidade"></param>
        public virtual void Inserir(TEntity entidade)
        {
            try
            {
                context.Set<TEntity>().Add(entidade);
                context.SaveChanges();

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        #endregion

        #region Alterar
        /// <summary>
        /// Alterar
        /// </summary>
        /// <param name="entidade"></param>
        public virtual void Alterar(TEntity entidade)
        {
            try
            {
                context.Entry(entidade).State = EntityState.Modified;
                context.SaveChanges();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion

        #region Excluir
        /// <summary>
        /// Excluir
        /// </summary>
        /// <param name="id"></param>
        public virtual void Excluir(int? id)
        {
            try
            {
                TEntity entity = Obter(id);

                context.Set<TEntity>().Remove(entity);
                context.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion

        #region DetachedLocal
        public virtual void DetachedLocal(Func<TEntity, bool> entidade)
        {
            var local = context.Set<TEntity>().Local.Where(entidade).FirstOrDefault();

            if (local != null)
            {
                context.Entry(local).State = EntityState.Detached;
            }
        }
        #endregion
    }
}