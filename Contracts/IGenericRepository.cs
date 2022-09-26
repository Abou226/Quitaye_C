﻿using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IGenericRepository<A>
    {
        Task<IEnumerable<A>> GetAll();
        Task<IEnumerable<A>> GetBy(Expression<Func<A, bool>> expression);

        Task<A> GetFirst(Expression<Func<A, bool>> expression, Expression<Func<A, bool>> ascending);
        Task<A> GetLast(Expression<Func<A, bool>> expression, Expression<Func<A, bool>> descending);

        Task<A> GetFirst(Expression<Func<A, bool>> ascending);
        Task<A> GetLast(Expression<Func<A, bool>> descending);

        Task<IEnumerable<A>> GetByDescendin(Expression<Func<A, bool>> expression, Expression<Func<A, bool>> descending);
        Task<IEnumerable<A>> GetByAscending(Expression<Func<A, bool>> expression, Expression<Func<A, bool>> ascending);
        Task<A> AddAsync(A value);
        Task<IEnumerable<A>> AddRangeAsync(List<A> value);
        Task<JsonPatchDocument> PatchUpdateAsync(Func<Action> action);
        void Update(A value);
        void Delete(A value);
    }

    public interface IGenericRepository<A, B>
    {
        Task<IEnumerable<A>> GetAll();
        Task<IEnumerable<A>> GetBy(Expression<Func<A, bool>> expression);
        Task<A> GetFirst(Expression<Func<A, bool>> expression, Expression<Func<A, bool>> ascending);
        Task<A> GetLast(Expression<Func<A, bool>> expression, Expression<Func<A, object>> descending);
        Task<A> GetFirst(Expression<Func<A, bool>> ascending);
        Task<A> GetLast(Expression<Func<A, bool>> descending);
        Task<IEnumerable<A>> GetBy(Expression<Func<A, B>> include);
        Task<IEnumerable<A>> GetBy(Expression<Func<A, bool>> expression, Expression<Func<A, B>> include);
        Task<IEnumerable<A>> GetByDescendin(Expression<Func<A, bool>> expression, Expression<Func<A, bool>> descending);
        Task<IEnumerable<A>> GetByAscending(Expression<Func<A, bool>> expression, Expression<Func<A, bool>> ascending);
        Task<A> AddAsync(A value);
        Task<JsonPatchDocument> PatchUpdateAsync(Func<Action> action);
        void Update(A value);
        void Delete(A value);
    }

    public interface IGenericRepository<A, B, C>
    {
        Task<IEnumerable<A>> GetAll();
        Task<IEnumerable<A>> GetBy(Expression<Func<A, bool>> expression);
        Task<A> GetFirst(Expression<Func<A, bool>> expression, Expression<Func<A, bool>> ascending);
        Task<A> GetLast(Expression<Func<A, bool>> expression, Expression<Func<A, bool>> descending);
        Task<A> GetFirst(Expression<Func<A, bool>> ascending);
        Task<A> GetLast(Expression<Func<A, bool>> descending);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include);
        Task<IEnumerable<A>> GetBy(Expression<Func<A, bool>> expression, Expression<Func<A, B>> include);
        Task<IEnumerable<A>> GetBy(Expression<Func<A, bool>> expression, Expression<Func<A, B>> include, Expression<Func<A, C>> include1);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, B>> include, Expression<Func<A, C>> include1);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include, Expression<Func<A, bool>> expression);
        Task<IEnumerable<A>> GetByDescendin(Expression<Func<A, bool>> expression, Expression<Func<A, bool>> descending);
        Task<IEnumerable<A>> GetByAscending(Expression<Func<A, bool>> expression, Expression<Func<A, bool>> ascending);

        Task<A> AddAsync(A value);
        Task<JsonPatchDocument> PatchUpdateAsync(Func<Action> action);
        void Update(A value);
        void Delete(A value);
    }

    public interface IGenericRepository<A, B, C, D>
    {
        Task<IEnumerable<A>> GetAll();
        Task<IEnumerable<A>> GetBy(Expression<Func<A, bool>> expression);
        Task<A> GetFirst(Expression<Func<A, bool>> expression, Expression<Func<A, bool>> ascending);
        Task<A> GetLast(Expression<Func<A, bool>> expression, Expression<Func<A, bool>> descending);
        Task<A> GetFirst(Expression<Func<A, bool>> ascending);
        Task<A> GetLast(Expression<Func<A, bool>> descending);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include, Expression<Func<A, D>> include1);
        Task<IEnumerable<A>> GetBy(Expression<Func<A, bool>> expression, Expression<Func<A, B>> include);
        Task<IEnumerable<A>> GetBy(Expression<Func<A, bool>> expression, Expression<Func<A, B>> include, Expression<Func<A, C>> include1);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include, Expression<Func<A, D>> include1);
        Task<IEnumerable<A>> GetBy(Expression<Func<A, bool>> expression, Expression<Func<A, B>> include, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, B>> include, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, bool>> expression);
        Task<IEnumerable<A>> GetByDescendin(Expression<Func<A, bool>> expression, Expression<Func<A, bool>> descending);
        Task<IEnumerable<A>> GetByAscending(Expression<Func<A, bool>> expression, Expression<Func<A, bool>> ascending);
        Task<A> AddAsync(A value);
        Task<JsonPatchDocument> PatchUpdateAsync(Func<Action> action);
        void Update(A value);
        void Delete(A value);
    }

    public interface IGenericRepository<A, B, C, D, E>
    {
        Task<IEnumerable<A>> GetAll();
        Task<IEnumerable<A>> GetBy(Expression<Func<A, bool>> expression);
        Task<A> GetFirst(Expression<Func<A, bool>> expression, Expression<Func<A, bool>> ascending);
        Task<A> GetLast(Expression<Func<A, bool>> expression, Expression<Func<A, bool>> descending);
        Task<A> GetFirst(Expression<Func<A, bool>> ascending);
        Task<A> GetLast(Expression<Func<A, bool>> descending);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include, Expression<Func<A, D>> include1);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, E>> include2);
        Task<IEnumerable<A>> GetBy(Expression<Func<A, bool>> expression, Expression<Func<A, B>> include);
        Task<IEnumerable<A>> GetBy(Expression<Func<A, bool>> expression, Expression<Func<A, B>> include, Expression<Func<A, C>> include1);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include, Expression<Func<A, D>> include1);
        Task<IEnumerable<A>> GetBy(Expression<Func<A, bool>> expression, Expression<Func<A, B>> include, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, E>> include2);
        Task<IEnumerable<A>> GetBy(Expression<Func<A, bool>> expression, Expression<Func<A, B>> include, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, B>> include, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, E>> include2, Expression<Func<A, bool>> expression);
        Task<IEnumerable<A>> GetByDescendin(Expression<Func<A, bool>> expression, Expression<Func<A, bool>> descending);
        Task<IEnumerable<A>> GetByAscending(Expression<Func<A, bool>> expression, Expression<Func<A, bool>> ascending);
        Task<A> AddAsync(A value);
        Task<JsonPatchDocument> PatchUpdateAsync(Func<Action> action);
        void Update(A value);
        void Delete(A value);
    }

    public interface IGenericRepository<A, B, C, D, E, F>
    {
        Task<IEnumerable<A>> GetAll();
        Task<IEnumerable<A>> GetBy(Expression<Func<A, bool>> expression);
        Task<A> GetFirst(Expression<Func<A, bool>> expression, Expression<Func<A, bool>> ascending);
        Task<A> GetLast(Expression<Func<A, bool>> expression, Expression<Func<A, bool>> descending);
        Task<A> GetFirst(Expression<Func<A, bool>> ascending);
        Task<A> GetLast(Expression<Func<A, bool>> descending);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include, Expression<Func<A, D>> include1);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, E>> include3, Expression<Func<A, F>> include2);

        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, E>> include3);
        Task<IEnumerable<A>> GetBy(Expression<Func<A, bool>> expression, Expression<Func<A, B>> include);
        Task<IEnumerable<A>> GetBy(Expression<Func<A, bool>> expression, Expression<Func<A, B>> include, Expression<Func<A, C>> include1);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include, Expression<Func<A, D>> include1);
        Task<IEnumerable<A>> GetBy(Expression<Func<A, bool>> expression, Expression<Func<A, B>> include, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, E>> include2);
        Task<IEnumerable<A>> GetBy(Expression<Func<A, bool>> expression, Expression<Func<A, B>> include, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, E>> include2, Expression<Func<A, F>> include3);
        Task<IEnumerable<A>> GetBy(Expression<Func<A, bool>> expression, Expression<Func<A, B>> include, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, B>> include, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, E>> include2, Expression<Func<A, F>> include3, Expression<Func<A, bool>> expression);
        Task<IEnumerable<A>> GetByDescendin(Expression<Func<A, bool>> expression, Expression<Func<A, bool>> descending);
        Task<IEnumerable<A>> GetByAscending(Expression<Func<A, bool>> expression, Expression<Func<A, bool>> ascending);
        Task<A> AddAsync(A value);
        Task<JsonPatchDocument> PatchUpdateAsync(Func<Action> action);
        void Update(A value);
        void Delete(A value);
    }

    public interface IGenericRepository<A, B, C, D, E, F, G>
    {
        Task<IEnumerable<A>> GetAll();
        Task<IEnumerable<A>> GetBy(Expression<Func<A, bool>> expression);
        Task<A> GetFirst(Expression<Func<A, bool>> expression, Expression<Func<A, bool>> ascending);
        Task<A> GetLast(Expression<Func<A, bool>> expression, Expression<Func<A, bool>> descending);
        Task<A> GetFirst(Expression<Func<A, bool>> ascending);
        Task<A> GetLast(Expression<Func<A, bool>> descending);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include, Expression<Func<A, D>> include1);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, E>> include3, Expression<Func<A, F>> include2);

        Task<IEnumerable<A>> GetBy(Expression<Func<A, bool>> expression, Expression<Func<A, B>> include);
        Task<IEnumerable<A>> GetBy(Expression<Func<A, bool>> expression, Expression<Func<A, B>> include, Expression<Func<A, C>> include1);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include, Expression<Func<A, D>> include1);
        Task<IEnumerable<A>> GetBy(Expression<Func<A, bool>> expression, Expression<Func<A, B>> include, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, E>> include2);
        Task<IEnumerable<A>> GetBy(Expression<Func<A, bool>> expression, Expression<Func<A, B>> include, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, E>> include2, Expression<Func<A, F>> include3);
        Task<IEnumerable<A>> GetBy(Expression<Func<A, bool>> expression, Expression<Func<A, B>> include, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, E>> include2, Expression<Func<A, F>> include3, Expression<Func<A, G>> include4);
        Task<IEnumerable<A>> GetByIncludeGroup(Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, E>> include2, Expression<Func<A, F>> include3, Expression<Func<A, G>> include4, Expression<Func<A, string>> expression);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, E>> include2, Expression<Func<A, F>> include3, Expression<Func<A, G>> include4, Expression<Func<A, bool>> expression);
        Task<IEnumerable<A>> GetBy(Expression<Func<A, bool>> expression, Expression<Func<A, B>> include, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5);

        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5);

        Task<IEnumerable<A>> GetByDescendin(Expression<Func<A, bool>> expression, Expression<Func<A, bool>> descending);
        Task<IEnumerable<A>> GetByAscending(Expression<Func<A, bool>> expression, Expression<Func<A, bool>> ascending);
        Task<A> AddAsync(A value);
        Task<JsonPatchDocument> PatchUpdateAsync(Func<Action> action);
        void Update(A value);
        void Delete(A value);
    }

    public interface IGenericRepository<A, B, C, D, E, F, G, H>
    {
        Task<IEnumerable<A>> GetAll();
        Task<IEnumerable<A>> GetBy(Expression<Func<A, bool>> expression);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include);
        Task<A> GetFirst(Expression<Func<A, bool>> expression, Expression<Func<A, bool>> ascending);
        Task<A> GetLast(Expression<Func<A, bool>> expression, Expression<Func<A, bool>> descending);
        Task<A> GetFirst(Expression<Func<A, bool>> ascending);
        Task<A> GetLast(Expression<Func<A, bool>> descending);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include, Expression<Func<A, D>> include1);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, E>> include2);

        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, E>> include2, Expression<Func<A, F>> include3);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, E>> include2, Expression<Func<A, F>> include3, Expression<Func<A, G>> include4);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, E>> include2, Expression<Func<A, F>> include3, Expression<Func<A, G>> include4, Expression<Func<A, H>> include5);
        Task<IEnumerable<A>> GetBy(Expression<Func<A, bool>> expression, Expression<Func<A, B>> include);
        Task<IEnumerable<A>> GetBy(Expression<Func<A, bool>> expression, Expression<Func<A, B>> include, Expression<Func<A, C>> include1);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include, Expression<Func<A, D>> include1);
        Task<IEnumerable<A>> GetBy(Expression<Func<A, bool>> expression, Expression<Func<A, B>> include, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, E>> include2);
        Task<IEnumerable<A>> GetBy(Expression<Func<A, bool>> expression, Expression<Func<A, B>> include, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, E>> include2, Expression<Func<A, F>> include3);
        Task<IEnumerable<A>> GetBy(Expression<Func<A, bool>> expression, Expression<Func<A, B>> include, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, E>> include2, Expression<Func<A, F>> include3, Expression<Func<A, G>> include4);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, E>> include2, Expression<Func<A, F>> include3, Expression<Func<A, G>> include4, Expression<Func<A, H>> include5);
        Task<IEnumerable<A>> GetByIncludeGroup(Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, E>> include2, Expression<Func<A, F>> include3, Expression<Func<A, G>> include4, Expression<Func<A, string>> expression);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, E>> include2, Expression<Func<A, F>> include3, Expression<Func<A, G>> include4, Expression<Func<A, bool>> expression);
        Task<IEnumerable<A>> GetBy(Expression<Func<A, bool>> expression, Expression<Func<A, B>> include, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5);

        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, bool>> expression);

        Task<IEnumerable<A>> GetByDescendin(Expression<Func<A, bool>> expression, Expression<Func<A, bool>> descending);
        Task<IEnumerable<A>> GetByAscending(Expression<Func<A, bool>> expression, Expression<Func<A, bool>> ascending);
        Task<A> AddAsync(A value);
        Task<JsonPatchDocument> PatchUpdateAsync(Func<Action> action);
        void Update(A value);
        void Delete(A value);
    }

    public interface IGenericRepository<A, B, C, D, E, F, G, H, I>
    {
        Task<IEnumerable<A>> GetAll();
        Task<IEnumerable<A>> GetBy(Expression<Func<A, bool>> expression);
        Task<A> GetFirst(Expression<Func<A, bool>> expression, Expression<Func<A, bool>> ascending);
        Task<A> GetLast(Expression<Func<A, bool>> expression, Expression<Func<A, bool>> descending);
        Task<A> GetFirst(Expression<Func<A, bool>> ascending);
        Task<A> GetLast(Expression<Func<A, bool>> descending);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include, Expression<Func<A, D>> include1);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, F>> include2);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, F>> include2, Expression<Func<A, G>> include3);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, F>> include2, Expression<Func<A, G>> include3, Expression<Func<A, H>> include4);
        Task<IEnumerable<A>> GetBy(Expression<Func<A, bool>> expression, Expression<Func<A, B>> include);
        Task<IEnumerable<A>> GetBy(Expression<Func<A, bool>> expression, Expression<Func<A, B>> include, Expression<Func<A, C>> include1);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include, Expression<Func<A, D>> include1);
        Task<IEnumerable<A>> GetBy(Expression<Func<A, bool>> expression, Expression<Func<A, B>> include, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, E>> include2);
        Task<IEnumerable<A>> GetBy(Expression<Func<A, bool>> expression, Expression<Func<A, B>> include, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, E>> include2, Expression<Func<A, F>> include3);
        Task<IEnumerable<A>> GetBy(Expression<Func<A, bool>> expression, Expression<Func<A, B>> include, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, E>> include2, Expression<Func<A, F>> include3, Expression<Func<A, G>> include4);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, E>> include2, Expression<Func<A, F>> include3, Expression<Func<A, G>> include4, Expression<Func<A, H>> include5);
        Task<IEnumerable<A>> GetByIncludeGroup(Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, E>> include2, Expression<Func<A, F>> include3, Expression<Func<A, G>> include4, Expression<Func<A, string>> expression);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, E>> include2, Expression<Func<A, F>> include3, Expression<Func<A, G>> include4, Expression<Func<A, bool>> expression);
        Task<IEnumerable<A>> GetBy(Expression<Func<A, bool>> expression, Expression<Func<A, B>> include, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5);

        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, bool>> expression);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7, Expression<Func<A, bool>> expression);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7);

        Task<IEnumerable<A>> GetByDescendin(Expression<Func<A, bool>> expression, Expression<Func<A, bool>> descending);
        Task<IEnumerable<A>> GetByAscending(Expression<Func<A, bool>> expression, Expression<Func<A, bool>> ascending);
        Task<A> AddAsync(A value);
        Task<JsonPatchDocument> PatchUpdateAsync(Func<Action> action);
        void Update(A value);
        void Delete(A value);
    }

    public interface IGenericRepository<A, B, C, D, E, F, G, H, I, J>
    {
        Task<IEnumerable<A>> GetAll();
        Task<IEnumerable<A>> GetBy(Expression<Func<A, bool>> expression);
        Task<A> GetFirst(Expression<Func<A, bool>> expression, Expression<Func<A, bool>> ascending);
        Task<A> GetLast(Expression<Func<A, bool>> expression, Expression<Func<A, bool>> descending);
        Task<A> GetFirst(Expression<Func<A, bool>> ascending);
        Task<A> GetLast(Expression<Func<A, bool>> descending);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include, Expression<Func<A, D>> include1);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, E>> include2);
        Task<IEnumerable<A>> GetByIncludeList(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, List<E>>> include2);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, E>> include2, Expression<Func<A, F>> include3);
        Task<IEnumerable<A>> GetByIncludeList(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, E>> include2, Expression<Func<A, List<F>>> include3);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, E>> include2, Expression<Func<A, F>> include3, Expression<Func<A, G>> include4);
        Task<IEnumerable<A>> GetByIncludeList(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, E>> include2, Expression<Func<A, F>> include3, Expression<Func<A, List<G>>> include4);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, E>> inlude2, Expression<Func<A, F>> include3, Expression<Func<A, G>> include4, Expression<Func<A, H>> include5);
        Task<IEnumerable<A>> GetByIncludeList(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, E>> inlude2, Expression<Func<A, F>> include3, Expression<Func<A, G>> include4, Expression<Func<A, List<H>>> include5);
        Task<IEnumerable<A>> GetBy(Expression<Func<A, bool>> expression, Expression<Func<A, B>> include);
        Task<IEnumerable<A>> GetBy(Expression<Func<A, bool>> expression, Expression<Func<A, B>> include, Expression<Func<A, C>> include1);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include, Expression<Func<A, D>> include1);
        Task<IEnumerable<A>> GetBy(Expression<Func<A, bool>> expression, Expression<Func<A, B>> include, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, E>> include2);
        Task<IEnumerable<A>> GetBy(Expression<Func<A, bool>> expression, Expression<Func<A, B>> include, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, E>> include2, Expression<Func<A, F>> include3);
        Task<IEnumerable<A>> GetBy(Expression<Func<A, bool>> expression, Expression<Func<A, B>> include, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, E>> include2, Expression<Func<A, F>> include3, Expression<Func<A, G>> include4);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, E>> include2, Expression<Func<A, F>> include3, Expression<Func<A, G>> include4, Expression<Func<A, H>> include5);
        Task<IEnumerable<A>> GetByIncludeGroup(Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, E>> include2, Expression<Func<A, F>> include3, Expression<Func<A, G>> include4, Expression<Func<A, string>> expression);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, E>> include2, Expression<Func<A, F>> include3, Expression<Func<A, G>> include4, Expression<Func<A, bool>> expression);
        Task<IEnumerable<A>> GetBy(Expression<Func<A, bool>> expression, Expression<Func<A, B>> include, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5);
        Task<IEnumerable<A>> GetBy(Expression<Func<A, bool>> expression, Expression<Func<A, B>> include, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, bool>> expression);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7, Expression<Func<A, J>> include8);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7, Expression<Func<A, J>> include9, Expression<Func<A, bool>> expression);

        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7, Expression<Func<A, bool>> expression);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7);
        Task<IEnumerable<A>> GetByIncludeList(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, List<I>>> include7);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7, Expression<Func<A, J>> include8);
        Task<IEnumerable<A>> GetByIncludeList(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7, Expression<Func<A, List<J>>> include8);

        Task<IEnumerable<A>> GetByDescendin(Expression<Func<A, bool>> expression, Expression<Func<A, bool>> descending);
        Task<IEnumerable<A>> GetByAscending(Expression<Func<A, bool>> expression, Expression<Func<A, bool>> ascending);
        Task<A> AddAsync(A value);
        Task<JsonPatchDocument> PatchUpdateAsync(Func<Action> action);
        void Update(A value);
        void Delete(A value);
    }

    public interface IGenericRepository<A, B, C, D, E, F, G, H, I, J, K>
    {
        Task<IEnumerable<A>> GetAll();
        Task<IEnumerable<A>> GetBy(Expression<Func<A, bool>> expression);
        Task<A> GetFirst(Expression<Func<A, bool>> expression, Expression<Func<A, bool>> ascending);
        Task<A> GetLast(Expression<Func<A, bool>> expression, Expression<Func<A, bool>> descending);
        Task<A> GetFirst(Expression<Func<A, bool>> ascending);
        Task<A> GetLast(Expression<Func<A, bool>> descending);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include, Expression<Func<A, D>> include1);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, F>> include2);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, F>> include2, Expression<Func<A, G>> include3);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, F>> include2, Expression<Func<A, G>> include3, Expression<Func<A, H>> include4);
        Task<IEnumerable<A>> GetBy(Expression<Func<A, bool>> expression, Expression<Func<A, B>> include);
        Task<IEnumerable<A>> GetBy(Expression<Func<A, bool>> expression, Expression<Func<A, B>> include, Expression<Func<A, C>> include1);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include, Expression<Func<A, D>> include1);
        Task<IEnumerable<A>> GetBy(Expression<Func<A, bool>> expression, Expression<Func<A, B>> include, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, E>> include2);
        Task<IEnumerable<A>> GetBy(Expression<Func<A, bool>> expression, Expression<Func<A, B>> include, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, E>> include2, Expression<Func<A, F>> include3);
        Task<IEnumerable<A>> GetBy(Expression<Func<A, bool>> expression, Expression<Func<A, B>> include, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, E>> include2, Expression<Func<A, F>> include3, Expression<Func<A, G>> include4);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, E>> include2, Expression<Func<A, F>> include3, Expression<Func<A, G>> include4, Expression<Func<A, H>> include5);
        Task<IEnumerable<A>> GetByIncludeGroup(Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, E>> include2, Expression<Func<A, F>> include3, Expression<Func<A, G>> include4, Expression<Func<A, string>> expression);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, E>> include2, Expression<Func<A, F>> include3, Expression<Func<A, G>> include4, Expression<Func<A, bool>> expression);
        Task<IEnumerable<A>> GetBy(Expression<Func<A, bool>> expression, Expression<Func<A, B>> include, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5);

        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, bool>> expression);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7, Expression<Func<A, J>> include8);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7, Expression<Func<A, J>> include9, Expression<Func<A, bool>> expression);

        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7, Expression<Func<A, bool>> expression);

        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7, Expression<Func<A, J>> include8, Expression<Func<A, K>> include9);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7, Expression<Func<A, K>> include8, Expression<Func<A, bool>> expression);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7, Expression<Func<A, J>> include8);

        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7, Expression<Func<A, J>> include8, Expression<Func<A, K>> include9, Expression<Func<A, bool>> expression);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7, Expression<Func<A, J>> include8, Expression<Func<A, K>> include9);

        Task<IEnumerable<A>> GetByDescendin(Expression<Func<A, bool>> expression, Expression<Func<A, bool>> descending);
        Task<IEnumerable<A>> GetByAscending(Expression<Func<A, bool>> expression, Expression<Func<A, bool>> ascending);
        Task<A> AddAsync(A value);
        Task<JsonPatchDocument> PatchUpdateAsync(Func<Action> action);
        void Update(A value);
        void Delete(A value);
    }

    public interface IGenericRepository<A, B, C, D, E, F, G, H, I, J, K, L>
    {
        Task<IEnumerable<A>> GetAll();
        Task<IEnumerable<A>> GetBy(Expression<Func<A, bool>> expression);
        Task<A> GetFirst(Expression<Func<A, bool>> expression, Expression<Func<A, bool>> ascending);
        Task<A> GetLast(Expression<Func<A, bool>> expression, Expression<Func<A, bool>> descending);
        Task<A> GetFirst(Expression<Func<A, bool>> ascending);
        Task<A> GetLast(Expression<Func<A, bool>> descending);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include, Expression<Func<A, D>> include1);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, F>> include2);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, F>> include2, Expression<Func<A, G>> include3);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, F>> include2, Expression<Func<A, G>> include3, Expression<Func<A, H>> include4);
        Task<IEnumerable<A>> GetBy(Expression<Func<A, bool>> expression, Expression<Func<A, B>> include);
        Task<IEnumerable<A>> GetBy(Expression<Func<A, bool>> expression, Expression<Func<A, B>> include, Expression<Func<A, C>> include1);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include, Expression<Func<A, D>> include1);
        Task<IEnumerable<A>> GetBy(Expression<Func<A, bool>> expression, Expression<Func<A, B>> include, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, E>> include2);
        Task<IEnumerable<A>> GetBy(Expression<Func<A, bool>> expression, Expression<Func<A, B>> include, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, E>> include2, Expression<Func<A, F>> include3);
        Task<IEnumerable<A>> GetBy(Expression<Func<A, bool>> expression, Expression<Func<A, B>> include, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, E>> include2, Expression<Func<A, F>> include3, Expression<Func<A, G>> include4);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, E>> include2, Expression<Func<A, F>> include3, Expression<Func<A, G>> include4, Expression<Func<A, H>> include5);
        Task<IEnumerable<A>> GetByIncludeGroup(Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, E>> include2, Expression<Func<A, F>> include3, Expression<Func<A, G>> include4, Expression<Func<A, string>> expression);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, E>> include2, Expression<Func<A, F>> include3, Expression<Func<A, G>> include4, Expression<Func<A, bool>> expression);
        Task<IEnumerable<A>> GetBy(Expression<Func<A, bool>> expression, Expression<Func<A, B>> include, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5);

        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, bool>> expression);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7, Expression<Func<A, J>> include8);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7, Expression<Func<A, J>> include9, Expression<Func<A, bool>> expression);

        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7, Expression<Func<A, bool>> expression);

        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7, Expression<Func<A, J>> include8, Expression<Func<A, K>> include9);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7, Expression<Func<A, K>> include8, Expression<Func<A, bool>> expression);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7, Expression<Func<A, J>> include8, Expression<Func<A, K>> include9, Expression<Func<A, bool>> expression);

        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7, Expression<Func<A, J>> include8, Expression<Func<A, K>> include9, Expression<Func<A, L>> include10, Expression<Func<A, bool>> expression);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7, Expression<Func<A, J>> include8);

        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7, Expression<Func<A, J>> include8, Expression<Func<A, K>> include9);

        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7, Expression<Func<A, J>> include8, Expression<Func<A, K>> include9, Expression<Func<A, L>> include10);


        Task<IEnumerable<A>> GetByDescendin(Expression<Func<A, bool>> expression, Expression<Func<A, bool>> descending);
        Task<IEnumerable<A>> GetByAscending(Expression<Func<A, bool>> expression, Expression<Func<A, bool>> ascending);
        Task<A> AddAsync(A value);
        Task<JsonPatchDocument> PatchUpdateAsync(Func<Action> action);
        void Update(A value);
        void Delete(A value);
    }

    public interface IGenericRepository<A, B, C, D, E, F, G, H, I, J, K, L, M>
    {
        Task<IEnumerable<A>> GetAll();
        Task<IEnumerable<A>> GetBy(Expression<Func<A, bool>> expression);
        Task<A> GetFirst(Expression<Func<A, bool>> expression, Expression<Func<A, bool>> ascending);
        Task<A> GetLast(Expression<Func<A, bool>> expression, Expression<Func<A, bool>> descending);
        Task<A> GetFirst(Expression<Func<A, bool>> ascending);
        Task<A> GetLast(Expression<Func<A, bool>> descending);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include, Expression<Func<A, D>> include1);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, F>> include2);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, F>> include2, Expression<Func<A, G>> include3);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, F>> include2, Expression<Func<A, G>> include3, Expression<Func<A, H>> include4);
        Task<IEnumerable<A>> GetBy(Expression<Func<A, bool>> expression, Expression<Func<A, B>> include);
        Task<IEnumerable<A>> GetBy(Expression<Func<A, bool>> expression, Expression<Func<A, B>> include, Expression<Func<A, C>> include1);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include, Expression<Func<A, D>> include1);
        Task<IEnumerable<A>> GetBy(Expression<Func<A, bool>> expression, Expression<Func<A, B>> include, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, E>> include2);
        Task<IEnumerable<A>> GetBy(Expression<Func<A, bool>> expression, Expression<Func<A, B>> include, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, E>> include2, Expression<Func<A, F>> include3);
        Task<IEnumerable<A>> GetBy(Expression<Func<A, bool>> expression, Expression<Func<A, B>> include, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, E>> include2, Expression<Func<A, F>> include3, Expression<Func<A, G>> include4);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, E>> include2, Expression<Func<A, F>> include3, Expression<Func<A, G>> include4, Expression<Func<A, H>> include5);
        Task<IEnumerable<A>> GetByIncludeGroup(Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, E>> include2, Expression<Func<A, F>> include3, Expression<Func<A, G>> include4, Expression<Func<A, string>> expression);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, E>> include2, Expression<Func<A, F>> include3, Expression<Func<A, G>> include4, Expression<Func<A, bool>> expression);
        Task<IEnumerable<A>> GetBy(Expression<Func<A, bool>> expression, Expression<Func<A, B>> include, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5);

        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, bool>> expression);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7, Expression<Func<A, J>> include8);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7, Expression<Func<A, J>> include9, Expression<Func<A, bool>> expression);

        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7, Expression<Func<A, bool>> expression);

        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7, Expression<Func<A, J>> include8, Expression<Func<A, K>> include9);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7, Expression<Func<A, K>> include8, Expression<Func<A, bool>> expression);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7, Expression<Func<A, J>> include8, Expression<Func<A, K>> include9, Expression<Func<A, bool>> expression);

        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7, Expression<Func<A, J>> include8, Expression<Func<A, K>> include9, Expression<Func<A, L>> include10, Expression<Func<A, bool>> expression);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7, Expression<Func<A, J>> include8);

        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7, Expression<Func<A, J>> include8, Expression<Func<A, K>> include9);

        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7, Expression<Func<A, J>> include8, Expression<Func<A, K>> include9, Expression<Func<A, L>> include10);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7, Expression<Func<A, J>> include8, Expression<Func<A, K>> include9, Expression<Func<A, L>> include10, Expression<Func<A, M>> include11);


        Task<IEnumerable<A>> GetByDescendin(Expression<Func<A, bool>> expression, Expression<Func<A, bool>> descending);
        Task<IEnumerable<A>> GetByAscending(Expression<Func<A, bool>> expression, Expression<Func<A, bool>> ascending);
        Task<A> AddAsync(A value);
        Task<JsonPatchDocument> PatchUpdateAsync(Func<Action> action);
        void Update(A value);
        void Delete(A value);
    }

    public interface IGenericRepository<A, B, C, D, E, F, G, H, I, J, K, L, M, N>
    {
        Task<IEnumerable<A>> GetAll();
        Task<IEnumerable<A>> GetBy(Expression<Func<A, bool>> expression);
        Task<A> GetFirst(Expression<Func<A, bool>> expression, Expression<Func<A, bool>> ascending);
        Task<A> GetLast(Expression<Func<A, bool>> expression, Expression<Func<A, bool>> descending);
        Task<A> GetFirst(Expression<Func<A, bool>> ascending);
        Task<A> GetLast(Expression<Func<A, bool>> descending);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include, Expression<Func<A, D>> include1);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, F>> include2);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, F>> include2, Expression<Func<A, G>> include3);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, F>> include2, Expression<Func<A, G>> include3, Expression<Func<A, H>> include4);
        Task<IEnumerable<A>> GetBy(Expression<Func<A, bool>> expression, Expression<Func<A, B>> include);
        Task<IEnumerable<A>> GetBy(Expression<Func<A, bool>> expression, Expression<Func<A, B>> include, Expression<Func<A, C>> include1);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include, Expression<Func<A, D>> include1);
        Task<IEnumerable<A>> GetBy(Expression<Func<A, bool>> expression, Expression<Func<A, B>> include, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, E>> include2);
        Task<IEnumerable<A>> GetBy(Expression<Func<A, bool>> expression, Expression<Func<A, B>> include, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, E>> include2, Expression<Func<A, F>> include3);
        Task<IEnumerable<A>> GetBy(Expression<Func<A, bool>> expression, Expression<Func<A, B>> include, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, E>> include2, Expression<Func<A, F>> include3, Expression<Func<A, G>> include4);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, E>> include2, Expression<Func<A, F>> include3, Expression<Func<A, G>> include4, Expression<Func<A, H>> include5);
        Task<IEnumerable<A>> GetByIncludeGroup(Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, E>> include2, Expression<Func<A, F>> include3, Expression<Func<A, G>> include4, Expression<Func<A, string>> expression);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, E>> include2, Expression<Func<A, F>> include3, Expression<Func<A, G>> include4, Expression<Func<A, bool>> expression);
        Task<IEnumerable<A>> GetBy(Expression<Func<A, bool>> expression, Expression<Func<A, B>> include, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5);

        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, bool>> expression);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7, Expression<Func<A, J>> include8);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7, Expression<Func<A, J>> include9, Expression<Func<A, bool>> expression);

        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7, Expression<Func<A, bool>> expression);

        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7, Expression<Func<A, J>> include8, Expression<Func<A, K>> include9);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7, Expression<Func<A, K>> include8, Expression<Func<A, bool>> expression);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7, Expression<Func<A, J>> include8, Expression<Func<A, K>> include9, Expression<Func<A, bool>> expression);

        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7, Expression<Func<A, J>> include8, Expression<Func<A, K>> include9, Expression<Func<A, L>> include10, Expression<Func<A, bool>> expression);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7, Expression<Func<A, J>> include8);

        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7, Expression<Func<A, J>> include8, Expression<Func<A, K>> include9);

        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7, Expression<Func<A, J>> include8, Expression<Func<A, K>> include9, Expression<Func<A, L>> include10);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7, Expression<Func<A, J>> include8, Expression<Func<A, K>> include9, Expression<Func<A, L>> include10, Expression<Func<A, M>> include11);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7, Expression<Func<A, J>> include8, Expression<Func<A, K>> include9, Expression<Func<A, L>> include10, Expression<Func<A, M>> include11, Expression<Func<A, N>> include12);


        Task<IEnumerable<A>> GetByDescendin(Expression<Func<A, bool>> expression, Expression<Func<A, bool>> descending);
        Task<IEnumerable<A>> GetByAscending(Expression<Func<A, bool>> expression, Expression<Func<A, bool>> ascending);
        Task<A> AddAsync(A value);
        Task<JsonPatchDocument> PatchUpdateAsync(Func<Action> action);
        void Update(A value);
        void Delete(A value);
    }
    public interface IGenericRepository<A, B, C, D, E, F, G, H, I, J, K, L, M, N, O>
    {
        Task<IEnumerable<A>> GetAll();
        Task<IEnumerable<A>> GetBy(Expression<Func<A, bool>> expression);
        Task<A> GetFirst(Expression<Func<A, bool>> expression, Expression<Func<A, bool>> ascending);
        Task<A> GetLast(Expression<Func<A, bool>> expression, Expression<Func<A, bool>> descending);
        Task<A> GetFirst(Expression<Func<A, bool>> ascending);
        Task<A> GetLast(Expression<Func<A, bool>> descending);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include, Expression<Func<A, D>> include1);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, F>> include2);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, F>> include2, Expression<Func<A, G>> include3);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, F>> include2, Expression<Func<A, G>> include3, Expression<Func<A, H>> include4);
        Task<IEnumerable<A>> GetBy(Expression<Func<A, bool>> expression, Expression<Func<A, B>> include);
        Task<IEnumerable<A>> GetBy(Expression<Func<A, bool>> expression, Expression<Func<A, B>> include, Expression<Func<A, C>> include1);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include, Expression<Func<A, D>> include1);
        Task<IEnumerable<A>> GetBy(Expression<Func<A, bool>> expression, Expression<Func<A, B>> include, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, E>> include2);
        Task<IEnumerable<A>> GetBy(Expression<Func<A, bool>> expression, Expression<Func<A, B>> include, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, E>> include2, Expression<Func<A, F>> include3);
        Task<IEnumerable<A>> GetBy(Expression<Func<A, bool>> expression, Expression<Func<A, B>> include, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, E>> include2, Expression<Func<A, F>> include3, Expression<Func<A, G>> include4);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, E>> include2, Expression<Func<A, F>> include3, Expression<Func<A, G>> include4, Expression<Func<A, H>> include5);
        Task<IEnumerable<A>> GetByIncludeGroup(Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, E>> include2, Expression<Func<A, F>> include3, Expression<Func<A, G>> include4, Expression<Func<A, string>> expression);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, E>> include2, Expression<Func<A, F>> include3, Expression<Func<A, G>> include4, Expression<Func<A, bool>> expression);
        Task<IEnumerable<A>> GetBy(Expression<Func<A, bool>> expression, Expression<Func<A, B>> include, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5);

        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, bool>> expression);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7, Expression<Func<A, J>> include8);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7, Expression<Func<A, J>> include9, Expression<Func<A, bool>> expression);

        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7, Expression<Func<A, bool>> expression);

        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7, Expression<Func<A, J>> include8, Expression<Func<A, K>> include9);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7, Expression<Func<A, K>> include8, Expression<Func<A, bool>> expression);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7, Expression<Func<A, J>> include8, Expression<Func<A, K>> include9, Expression<Func<A, bool>> expression);

        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7, Expression<Func<A, J>> include8, Expression<Func<A, K>> include9, Expression<Func<A, L>> include10, Expression<Func<A, bool>> expression);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7, Expression<Func<A, J>> include8);

        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7, Expression<Func<A, J>> include8, Expression<Func<A, K>> include9);

        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7, Expression<Func<A, J>> include8, Expression<Func<A, K>> include9, Expression<Func<A, L>> include10);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7, Expression<Func<A, J>> include8, Expression<Func<A, K>> include9, Expression<Func<A, L>> include10, Expression<Func<A, M>> include11);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7, Expression<Func<A, J>> include8, Expression<Func<A, K>> include9, Expression<Func<A, L>> include10, Expression<Func<A, M>> include11, Expression<Func<A, N>> include12);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7, Expression<Func<A, J>> include8, Expression<Func<A, K>> include9, Expression<Func<A, L>> include10, Expression<Func<A, M>> include11, Expression<Func<A, N>> include12, Expression<Func<A, O>> include13);


        Task<IEnumerable<A>> GetByDescendin(Expression<Func<A, bool>> expression, Expression<Func<A, bool>> descending);
        Task<IEnumerable<A>> GetByAscending(Expression<Func<A, bool>> expression, Expression<Func<A, bool>> ascending);
        Task<A> AddAsync(A value);
        Task<JsonPatchDocument> PatchUpdateAsync(Func<Action> action);
        void Update(A value);
        void Delete(A value);
    }

    public interface IGenericRepository<A, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P>
    {
        Task<IEnumerable<A>> GetAll();
        Task<IEnumerable<A>> GetBy(Expression<Func<A, bool>> expression);
        Task<A> GetFirst(Expression<Func<A, bool>> expression, Expression<Func<A, bool>> ascending);
        Task<A> GetLast(Expression<Func<A, bool>> expression, Expression<Func<A, bool>> descending);
        Task<A> GetFirst(Expression<Func<A, bool>> ascending);
        Task<A> GetLast(Expression<Func<A, bool>> descending);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include, Expression<Func<A, D>> include1);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, F>> include2);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, F>> include2, Expression<Func<A, G>> include3);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, F>> include2, Expression<Func<A, G>> include3, Expression<Func<A, H>> include4);
        Task<IEnumerable<A>> GetBy(Expression<Func<A, bool>> expression, Expression<Func<A, B>> include);
        Task<IEnumerable<A>> GetBy(Expression<Func<A, bool>> expression, Expression<Func<A, B>> include, Expression<Func<A, C>> include1);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include, Expression<Func<A, D>> include1);
        Task<IEnumerable<A>> GetBy(Expression<Func<A, bool>> expression, Expression<Func<A, B>> include, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, E>> include2);
        Task<IEnumerable<A>> GetBy(Expression<Func<A, bool>> expression, Expression<Func<A, B>> include, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, E>> include2, Expression<Func<A, F>> include3);
        Task<IEnumerable<A>> GetBy(Expression<Func<A, bool>> expression, Expression<Func<A, B>> include, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, E>> include2, Expression<Func<A, F>> include3, Expression<Func<A, G>> include4);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, E>> include2, Expression<Func<A, F>> include3, Expression<Func<A, G>> include4, Expression<Func<A, H>> include5);
        Task<IEnumerable<A>> GetByIncludeGroup(Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, E>> include2, Expression<Func<A, F>> include3, Expression<Func<A, G>> include4, Expression<Func<A, string>> expression);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, E>> include2, Expression<Func<A, F>> include3, Expression<Func<A, G>> include4, Expression<Func<A, bool>> expression);
        Task<IEnumerable<A>> GetBy(Expression<Func<A, bool>> expression, Expression<Func<A, B>> include, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5);

        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, bool>> expression);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7, Expression<Func<A, J>> include8);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7, Expression<Func<A, J>> include9, Expression<Func<A, bool>> expression);

        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7, Expression<Func<A, bool>> expression);

        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7, Expression<Func<A, J>> include8, Expression<Func<A, K>> include9);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7, Expression<Func<A, K>> include8, Expression<Func<A, bool>> expression);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7, Expression<Func<A, J>> include8, Expression<Func<A, K>> include9, Expression<Func<A, bool>> expression);

        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7, Expression<Func<A, J>> include8, Expression<Func<A, K>> include9, Expression<Func<A, L>> include10, Expression<Func<A, bool>> expression);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7, Expression<Func<A, J>> include8);

        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7, Expression<Func<A, J>> include8, Expression<Func<A, K>> include9);

        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7, Expression<Func<A, J>> include8, Expression<Func<A, K>> include9, Expression<Func<A, L>> include10);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7, Expression<Func<A, J>> include8, Expression<Func<A, K>> include9, Expression<Func<A, L>> include10, Expression<Func<A, M>> include11);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7, Expression<Func<A, J>> include8, Expression<Func<A, K>> include9, Expression<Func<A, L>> include10, Expression<Func<A, M>> include11, Expression<Func<A, N>> include12);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7, Expression<Func<A, J>> include8, Expression<Func<A, K>> include9, Expression<Func<A, L>> include10, Expression<Func<A, M>> include11, Expression<Func<A, N>> include12, Expression<Func<A, O>> include13);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7, Expression<Func<A, J>> include8, Expression<Func<A, K>> include9, Expression<Func<A, L>> include10, Expression<Func<A, M>> include11, Expression<Func<A, N>> include12, Expression<Func<A, O>> include13, Expression<Func<A, P>> include14);


        Task<IEnumerable<A>> GetByDescendin(Expression<Func<A, bool>> expression, Expression<Func<A, bool>> descending);
        Task<IEnumerable<A>> GetByAscending(Expression<Func<A, bool>> expression, Expression<Func<A, bool>> ascending);
        Task<A> AddAsync(A value);
        Task<JsonPatchDocument> PatchUpdateAsync(Func<Action> action);
        void Update(A value);
        void Delete(A value);
    }

    public interface IGenericRepository<A, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P, Q>
    {
        Task<IEnumerable<A>> GetAll();
        Task<IEnumerable<A>> GetBy(Expression<Func<A, bool>> expression);
        Task<A> GetFirst(Expression<Func<A, bool>> expression, Expression<Func<A, bool>> ascending);
        Task<A> GetLast(Expression<Func<A, bool>> expression, Expression<Func<A, bool>> descending);
        Task<A> GetFirst(Expression<Func<A, bool>> ascending);
        Task<A> GetLast(Expression<Func<A, bool>> descending);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include, Expression<Func<A, D>> include1);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, F>> include2);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, F>> include2, Expression<Func<A, G>> include3);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, F>> include2, Expression<Func<A, G>> include3, Expression<Func<A, H>> include4);
        Task<IEnumerable<A>> GetBy(Expression<Func<A, bool>> expression, Expression<Func<A, B>> include);
        Task<IEnumerable<A>> GetBy(Expression<Func<A, bool>> expression, Expression<Func<A, B>> include, Expression<Func<A, C>> include1);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include, Expression<Func<A, D>> include1);
        Task<IEnumerable<A>> GetBy(Expression<Func<A, bool>> expression, Expression<Func<A, B>> include, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, E>> include2);
        Task<IEnumerable<A>> GetBy(Expression<Func<A, bool>> expression, Expression<Func<A, B>> include, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, E>> include2, Expression<Func<A, F>> include3);
        Task<IEnumerable<A>> GetBy(Expression<Func<A, bool>> expression, Expression<Func<A, B>> include, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, E>> include2, Expression<Func<A, F>> include3, Expression<Func<A, G>> include4);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, E>> include2, Expression<Func<A, F>> include3, Expression<Func<A, G>> include4, Expression<Func<A, H>> include5);
        Task<IEnumerable<A>> GetByIncludeGroup(Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, E>> include2, Expression<Func<A, F>> include3, Expression<Func<A, G>> include4, Expression<Func<A, string>> expression);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, E>> include2, Expression<Func<A, F>> include3, Expression<Func<A, G>> include4, Expression<Func<A, bool>> expression);
        Task<IEnumerable<A>> GetBy(Expression<Func<A, bool>> expression, Expression<Func<A, B>> include, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5);

        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, bool>> expression);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7, Expression<Func<A, J>> include8);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7, Expression<Func<A, J>> include9, Expression<Func<A, bool>> expression);

        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7, Expression<Func<A, bool>> expression);

        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7, Expression<Func<A, J>> include8, Expression<Func<A, K>> include9);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7, Expression<Func<A, K>> include8, Expression<Func<A, bool>> expression);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7, Expression<Func<A, J>> include8, Expression<Func<A, K>> include9, Expression<Func<A, bool>> expression);

        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7, Expression<Func<A, J>> include8, Expression<Func<A, K>> include9, Expression<Func<A, L>> include10, Expression<Func<A, bool>> expression);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7, Expression<Func<A, J>> include8);

        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7, Expression<Func<A, J>> include8, Expression<Func<A, K>> include9);

        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7, Expression<Func<A, J>> include8, Expression<Func<A, K>> include9, Expression<Func<A, L>> include10);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7, Expression<Func<A, J>> include8, Expression<Func<A, K>> include9, Expression<Func<A, L>> include10, Expression<Func<A, M>> include11);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7, Expression<Func<A, J>> include8, Expression<Func<A, K>> include9, Expression<Func<A, L>> include10, Expression<Func<A, M>> include11, Expression<Func<A, N>> include12);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7, Expression<Func<A, J>> include8, Expression<Func<A, K>> include9, Expression<Func<A, L>> include10, Expression<Func<A, M>> include11, Expression<Func<A, N>> include12, Expression<Func<A, O>> include13);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7, Expression<Func<A, J>> include8, Expression<Func<A, K>> include9, Expression<Func<A, L>> include10, Expression<Func<A, M>> include11, Expression<Func<A, N>> include12, Expression<Func<A, O>> include13, Expression<Func<A, P>> include14);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7, Expression<Func<A, J>> include8, Expression<Func<A, K>> include9, Expression<Func<A, L>> include10, Expression<Func<A, M>> include11, Expression<Func<A, N>> include12, Expression<Func<A, O>> include13, Expression<Func<A, P>> include14, Expression<Func<A, Q>> include15);


        Task<IEnumerable<A>> GetByDescendin(Expression<Func<A, bool>> expression, Expression<Func<A, bool>> descending);
        Task<IEnumerable<A>> GetByAscending(Expression<Func<A, bool>> expression, Expression<Func<A, bool>> ascending);
        Task<A> AddAsync(A value);
        Task<JsonPatchDocument> PatchUpdateAsync(Func<Action> action);
        void Update(A value);
        void Delete(A value);
    }

    public interface IGenericRepository<A, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P, Q, R>
    {
        Task<IEnumerable<A>> GetAll();
        Task<IEnumerable<A>> GetBy(Expression<Func<A, bool>> expression);
        Task<A> GetFirst(Expression<Func<A, bool>> expression, Expression<Func<A, bool>> ascending);
        Task<A> GetLast(Expression<Func<A, bool>> expression, Expression<Func<A, bool>> descending);
        Task<A> GetFirst(Expression<Func<A, bool>> ascending);
        Task<A> GetLast(Expression<Func<A, bool>> descending);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include, Expression<Func<A, D>> include1);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, F>> include2);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, F>> include2, Expression<Func<A, G>> include3);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, F>> include2, Expression<Func<A, G>> include3, Expression<Func<A, H>> include4);
        Task<IEnumerable<A>> GetBy(Expression<Func<A, bool>> expression, Expression<Func<A, B>> include);
        Task<IEnumerable<A>> GetBy(Expression<Func<A, bool>> expression, Expression<Func<A, B>> include, Expression<Func<A, C>> include1);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include, Expression<Func<A, D>> include1);
        Task<IEnumerable<A>> GetBy(Expression<Func<A, bool>> expression, Expression<Func<A, B>> include, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, E>> include2);
        Task<IEnumerable<A>> GetBy(Expression<Func<A, bool>> expression, Expression<Func<A, B>> include, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, E>> include2, Expression<Func<A, F>> include3);
        Task<IEnumerable<A>> GetBy(Expression<Func<A, bool>> expression, Expression<Func<A, B>> include, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, E>> include2, Expression<Func<A, F>> include3, Expression<Func<A, G>> include4);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, E>> include2, Expression<Func<A, F>> include3, Expression<Func<A, G>> include4, Expression<Func<A, H>> include5);
        Task<IEnumerable<A>> GetByIncludeGroup(Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, E>> include2, Expression<Func<A, F>> include3, Expression<Func<A, G>> include4, Expression<Func<A, string>> expression);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include, Expression<Func<A, D>> include1, Expression<Func<A, E>> include2, Expression<Func<A, F>> include3, Expression<Func<A, G>> include4, Expression<Func<A, bool>> expression);
        Task<IEnumerable<A>> GetBy(Expression<Func<A, bool>> expression, Expression<Func<A, B>> include, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5);

        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, bool>> expression);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7, Expression<Func<A, J>> include8);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7, Expression<Func<A, J>> include9, Expression<Func<A, bool>> expression);

        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7, Expression<Func<A, bool>> expression);

        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7, Expression<Func<A, J>> include8, Expression<Func<A, K>> include9);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7, Expression<Func<A, K>> include8, Expression<Func<A, bool>> expression);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7, Expression<Func<A, J>> include8, Expression<Func<A, K>> include9, Expression<Func<A, bool>> expression);

        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7, Expression<Func<A, J>> include8, Expression<Func<A, K>> include9, Expression<Func<A, L>> include10, Expression<Func<A, bool>> expression);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7, Expression<Func<A, J>> include8);

        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7, Expression<Func<A, J>> include8, Expression<Func<A, K>> include9);

        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7, Expression<Func<A, J>> include8, Expression<Func<A, K>> include9, Expression<Func<A, L>> include10);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7, Expression<Func<A, J>> include8, Expression<Func<A, K>> include9, Expression<Func<A, L>> include10, Expression<Func<A, M>> include11);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7, Expression<Func<A, J>> include8, Expression<Func<A, K>> include9, Expression<Func<A, L>> include10, Expression<Func<A, M>> include11, Expression<Func<A, N>> include12);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7, Expression<Func<A, J>> include8, Expression<Func<A, K>> include9, Expression<Func<A, L>> include10, Expression<Func<A, M>> include11, Expression<Func<A, N>> include12, Expression<Func<A, O>> include13);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7, Expression<Func<A, J>> include8, Expression<Func<A, K>> include9, Expression<Func<A, L>> include10, Expression<Func<A, M>> include11, Expression<Func<A, N>> include12, Expression<Func<A, O>> include13, Expression<Func<A, P>> include14);
        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7, Expression<Func<A, J>> include8, Expression<Func<A, K>> include9, Expression<Func<A, L>> include10, Expression<Func<A, M>> include11, Expression<Func<A, N>> include12, Expression<Func<A, O>> include13, Expression<Func<A, P>> include14, Expression<Func<A, Q>> include15);

        Task<IEnumerable<A>> GetByInclude(Expression<Func<A, bool>> expression, Expression<Func<A, C>> include1, Expression<Func<A, D>> include2, Expression<Func<A, E>> include3, Expression<Func<A, F>> include4, Expression<Func<A, G>> include5, Expression<Func<A, H>> include6, Expression<Func<A, I>> include7, Expression<Func<A, J>> include8, Expression<Func<A, K>> include9, Expression<Func<A, L>> include10, Expression<Func<A, M>> include11, Expression<Func<A, N>> include12, Expression<Func<A, O>> include13, Expression<Func<A, P>> include14, Expression<Func<A, Q>> include15, Expression<Func<A, R>> include16);


        Task<IEnumerable<A>> GetByDescendin(Expression<Func<A, bool>> expression, Expression<Func<A, bool>> descending);
        Task<IEnumerable<A>> GetByAscending(Expression<Func<A, bool>> expression, Expression<Func<A, bool>> ascending);
        Task<A> AddAsync(A value);
        Task<JsonPatchDocument> PatchUpdateAsync(Func<Action> action);
        void Update(A value);
        void Delete(A value);
    }
}
