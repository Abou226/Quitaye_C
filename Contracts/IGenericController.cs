﻿using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IGenericController<A> where A : class
    {
        Task<ActionResult<IEnumerable<A>>> GetAll();
        Task<ActionResult<IEnumerable<A>>> GetBy(string search);
        Task<ActionResult<IEnumerable<A>>> GetBy(string search, DateTime start, DateTime end);
        Task<ActionResult<IEnumerable<A>>> AddAsync(List<A> values);
        Task<ActionResult<A>> UpdateAsync(A value);
        Task<ActionResult<A>> PatchUpdateAsync(JsonPatchDocument value, Guid id);
        Task<ActionResult<A>> Delete(Guid id);
    }

    public interface IGenericController<A, B> where A : class where B : class
    {
        Task<ActionResult<IEnumerable<A>>> GetAll();
        Task<ActionResult<IEnumerable<A>>> GetBy(string search);
        Task<ActionResult<IEnumerable<A>>> GetBy(string search, DateTime start, DateTime end);
        Task<ActionResult<IEnumerable<A>>> AddAsync(List<A> values);
        Task<ActionResult<A>> UpdateAsync(A value);
        Task<ActionResult<A>> Delete(Guid id);
    }

    public interface IGenericController<A, B, C> where A : class where B : class where C : class
    {
        Task<ActionResult<IEnumerable<A>>> GetAll();
        Task<ActionResult<IEnumerable<A>>> GetBy(string search);
        Task<ActionResult<IEnumerable<A>>> GetBy(string search, DateTime start, DateTime end);
        Task<ActionResult<IEnumerable<A>>> AddAsync(List<A> values);
        Task<ActionResult<A>> UpdateAsync(A value);
        Task<ActionResult<A>> Delete(Guid id);
    }

    public interface IGenericController<A, B, C, D> where A : class where B : class where C : class where D : class
    {
        Task<ActionResult<IEnumerable<A>>> GetAll();
        Task<ActionResult<IEnumerable<A>>> GetBy(string search);
        Task<ActionResult<IEnumerable<A>>> GetBy(string search, DateTime start, DateTime end);
        Task<ActionResult<IEnumerable<A>>> AddAsync(List<A> values);
        Task<ActionResult<A>> UpdateAsync(A value);
        Task<ActionResult<A>> PatchUpdateAsync(JsonPatchDocument value, Guid id);
        Task<ActionResult<A>> Delete(Guid id);
    }

    public interface IGenericController<A, B, C, D, E> where A : class where B : class where C : class where D : class where E : class
    {
        Task<ActionResult<IEnumerable<A>>> GetAll();
        Task<ActionResult<IEnumerable<A>>> GetBy(string search);
        Task<ActionResult<IEnumerable<A>>> GetBy(string search, DateTime start, DateTime end);
        Task<ActionResult<IEnumerable<A>>> AddAsync(List<A> values);
        Task<ActionResult<A>> UpdateAsync(A value);
        Task<ActionResult<A>> PatchUpdateAsync(JsonPatchDocument value, Guid id);
        Task<ActionResult<A>> Delete(Guid id);
    }

    public interface IGenericController<A, B, C, D, E, F> where A : class where B : class where C : class where D : class where E : class where F : class
    {
        Task<ActionResult<IEnumerable<A>>> GetAll();
        Task<ActionResult<IEnumerable<A>>> GetBy(string search);
        Task<ActionResult<IEnumerable<A>>> GetBy(string search, DateTime start, DateTime end);
        Task<ActionResult<IEnumerable<A>>> AddAsync(List<A> values);
        Task<ActionResult<A>> UpdateAsync(A value);
        Task<ActionResult<A>> PatchUpdateAsync(JsonPatchDocument value, Guid id);
        Task<ActionResult<A>> Delete(Guid id);
    }

    public interface IGenericController<A, B, C, D, E, F, G> where A : class where B : class where C : class where D : class where E : class where F : class where G : class
    {
        Task<ActionResult<IEnumerable<A>>> GetAll();
        Task<ActionResult<IEnumerable<A>>> GetBy(string search);
        Task<ActionResult<IEnumerable<A>>> GetBy(string search, DateTime start, DateTime end);
        Task<ActionResult<IEnumerable<A>>> AddAsync(List<A> values);
        Task<ActionResult<A>> UpdateAsync(A value);
        Task<ActionResult<A>> PatchUpdateAsync(JsonPatchDocument value, Guid id);
        Task<ActionResult<A>> Delete(Guid id);
    }

    public interface IGenericController<A, B, C, D, E, F, G, H> where A : class where B : class where C : class where D : class where E : class where F : class where G : class where H : class
    {
        Task<ActionResult<IEnumerable<A>>> GetAll();
        Task<ActionResult<IEnumerable<A>>> GetBy(string search);
        Task<ActionResult<IEnumerable<A>>> GetBy(string search, DateTime start, DateTime end);
        Task<ActionResult<IEnumerable<A>>> AddAsync(List<A> values);
        Task<ActionResult<A>> UpdateAsync(A value);
        Task<ActionResult<A>> PatchUpdateAsync(JsonPatchDocument value, Guid id);
        Task<ActionResult<A>> Delete(Guid id);
    }

    public interface IGenericController<A, B, C, D, E, F, G, H, I> where A : class where B : class where C : class where D : class where E : class where F : class where G : class where H : class where I : class
    {
        Task<ActionResult<IEnumerable<A>>> GetAll();
        Task<ActionResult<IEnumerable<A>>> GetBy(string search);
        Task<ActionResult<IEnumerable<A>>> GetBy(string search, DateTime start, DateTime end);
        Task<ActionResult<IEnumerable<A>>> AddAsync(List<A> values);
        Task<ActionResult<A>> UpdateAsync(A value);
        Task<ActionResult<A>> PatchUpdateAsync(JsonPatchDocument value, Guid id);
        Task<ActionResult<A>> Delete(Guid id);
    }

    public interface IGenericController<A, B, C, D, E, F, G, H, I, J> where A : class where B : class where C : class where D : class where E : class where F : class where G : class where H : class where I : class where J : class
    {
        Task<ActionResult<IEnumerable<A>>> GetAll();
        Task<ActionResult<IEnumerable<A>>> GetBy(string search);
        Task<ActionResult<IEnumerable<A>>> GetBy(string search, DateTime start, DateTime end);
        Task<ActionResult<IEnumerable<A>>> AddAsync(List<A> values);
        Task<ActionResult<A>> UpdateAsync(A value);
        Task<ActionResult<A>> PatchUpdateAsync(JsonPatchDocument value, Guid id);
        Task<ActionResult<A>> Delete(Guid id);
    }

    public interface IGenericController<A, B, C, D, E, F, G, H, I, J, K> where A : class where B : class where C : class where D : class where E : class where F : class where G : class where H : class where I : class where J : class where K : class
    {
        Task<ActionResult<IEnumerable<A>>> GetAll();
        Task<ActionResult<IEnumerable<A>>> GetBy(string search);
        Task<ActionResult<IEnumerable<A>>> GetBy(string search, DateTime start, DateTime end);
        Task<ActionResult<IEnumerable<A>>> AddAsync(List<A> values);
        Task<ActionResult<A>> UpdateAsync(A value);
        Task<ActionResult<A>> PatchUpdateAsync(JsonPatchDocument value, Guid id);
        Task<ActionResult<A>> Delete(Guid id);
    }

    public interface IGenericController<A, B, C, D, E, F, G, H, I, J, K, L> where A : class where B : class where C : class where D : class where E : class where F : class where G : class where H : class where I : class where J : class where K : class where L : class
    {
        Task<ActionResult<IEnumerable<A>>> GetAll();
        Task<ActionResult<IEnumerable<A>>> GetBy(string search);
        Task<ActionResult<IEnumerable<A>>> GetBy(string search, DateTime start, DateTime end);
        Task<ActionResult<IEnumerable<A>>> AddAsync(List<A> values);
        Task<ActionResult<A>> UpdateAsync(A value);
        Task<ActionResult<A>> PatchUpdateAsync(JsonPatchDocument value, Guid id);
        Task<ActionResult<A>> Delete(Guid id);
    }

    public interface IGenericController<A, B, C, D, E, F, G, H, I, J, K, L, M> where A : class where B : class 
        where C : class where D : class where E : class where F : class 
        where G : class where H : class where I : class where J : class where K : class where L : class where M : class
    {
        Task<ActionResult<IEnumerable<A>>> GetAll();
        Task<ActionResult<IEnumerable<A>>> GetBy(string search);
        Task<ActionResult<IEnumerable<A>>> GetBy(string search, DateTime start, DateTime end);
        Task<ActionResult<IEnumerable<A>>> AddAsync(List<A> values);
        Task<ActionResult<A>> UpdateAsync(A value);
        Task<ActionResult<A>> PatchUpdateAsync(JsonPatchDocument value, Guid id);
        Task<ActionResult<A>> Delete(Guid id);
    }

    public interface IGenericController<A, B, C, D, E, F, G, H, I, J, K, L, M, N> 
        where A : class where B : class
        where C : class where D : class where E : class where F : class
        where G : class where H : class where I : class where J : class 
        where K : class where L : class where M : class where N : class
    {
        Task<ActionResult<IEnumerable<A>>> GetAll();
        Task<ActionResult<IEnumerable<A>>> GetBy(string search);
        Task<ActionResult<IEnumerable<A>>> GetBy(string search, DateTime start, DateTime end);
        Task<ActionResult<IEnumerable<A>>> AddAsync(List<A> values);
        Task<ActionResult<A>> UpdateAsync(A value);
        Task<ActionResult<A>> PatchUpdateAsync(JsonPatchDocument value, Guid id);
        Task<ActionResult<A>> Delete(Guid id);
    }

    public interface IGenericController<A, B, C, D, E, F, G, H, I, J, K, L, M, N, O>
        where A : class where B : class
        where C : class where D : class where E : class where F : class
        where G : class where H : class where I : class where J : class
        where K : class where L : class where M : class where N : class where O : class
    {
        Task<ActionResult<IEnumerable<A>>> GetAll();
        Task<ActionResult<IEnumerable<A>>> GetBy(string search);
        Task<ActionResult<IEnumerable<A>>> GetBy(string search, DateTime start, DateTime end);
        Task<ActionResult<IEnumerable<A>>> AddAsync(List<A> values);
        Task<ActionResult<A>> UpdateAsync(A value);
        Task<ActionResult<A>> PatchUpdateAsync(JsonPatchDocument value, Guid id);
        Task<ActionResult<A>> Delete(Guid id);
    }

    public interface IGenericController<A, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P>
        where A : class where B : class
        where C : class where D : class where E : class where F : class
        where G : class where H : class where I : class where J : class
        where K : class where L : class where M : class where N : class 
        where O : class where P : class
    {
        Task<ActionResult<IEnumerable<A>>> GetAll();
        Task<ActionResult<IEnumerable<A>>> GetBy(string search);
        Task<ActionResult<IEnumerable<A>>> GetBy(string search, DateTime start, DateTime end);
        Task<ActionResult<IEnumerable<A>>> AddAsync(List<A> values);
        Task<ActionResult<A>> UpdateAsync(A value);
        Task<ActionResult<A>> PatchUpdateAsync(JsonPatchDocument value, Guid id);
        Task<ActionResult<A>> Delete(Guid id);
    }

    public interface IGenericController<A, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P, Q>
        where A : class where B : class
        where C : class where D : class where E : class where F : class
        where G : class where H : class where I : class where J : class
        where K : class where L : class where M : class where N : class
        where O : class where P : class where Q : class
    {
        Task<ActionResult<IEnumerable<A>>> GetAll();
        Task<ActionResult<IEnumerable<A>>> GetBy(string search);
        Task<ActionResult<IEnumerable<A>>> GetBy(string search, DateTime start, DateTime end);
        Task<ActionResult<IEnumerable<A>>> AddAsync(List<A> values);
        Task<ActionResult<A>> UpdateAsync(A value);
        Task<ActionResult<A>> PatchUpdateAsync(JsonPatchDocument value, Guid id);
        Task<ActionResult<A>> Delete(Guid id);
    }

    public interface IGenericController<A, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P, Q, R>
        where A : class where B : class
        where C : class where D : class where E : class where F : class
        where G : class where H : class where I : class where J : class
        where K : class where L : class where M : class where N : class
        where O : class where P : class where Q : class where R : class
    {
        Task<ActionResult<IEnumerable<A>>> GetAll();
        Task<ActionResult<IEnumerable<A>>> GetBy(string search);
        Task<ActionResult<IEnumerable<A>>> GetBy(string search, DateTime start, DateTime end);
        Task<ActionResult<IEnumerable<A>>> AddAsync(List<A> values);
        Task<ActionResult<A>> UpdateAsync(A value);
        Task<ActionResult<A>> PatchUpdateAsync(JsonPatchDocument value, Guid id);
        Task<ActionResult<A>> Delete(Guid id);
    }
}
