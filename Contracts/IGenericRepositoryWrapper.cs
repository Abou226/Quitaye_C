using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IGenericRepositoryWrapper<A> 
    {
        IGenericRepository<A> Item { get; }

        Task SaveAsync();
    }

    public interface IGenericRepositoryWrapper<A, B>
    {
        IGenericRepository<A, B> Item { get; }
        IGenericRepository<A> ItemA { get; }
        IGenericRepository<B> ItemB { get; }
        Task SaveAsync();
    }

    public interface IGenericRepositoryWrapper<A, B, C>
    {
        IGenericRepository<A, B, C> Item { get; }
        IGenericRepository<A> ItemA { get; }
        IGenericRepository<B> ItemB { get; }
        IGenericRepository<C> ItemC { get; }

        Task SaveAsync();
    }

    public interface IGenericRepositoryWrapper<A, B, C, D>
    {
        IGenericRepository<A, B, C, D> Item { get; }
        IGenericRepository<A> ItemA { get; }
        IGenericRepository<B> ItemB { get; }
        IGenericRepository<C> ItemC { get; }
        IGenericRepository<D> ItemD { get; }

        Task SaveAsync();
    }

    public interface IGenericRepositoryWrapper<A, B, C, D, E>
    {

        IGenericRepository<A, B, C, D, E> Item { get; }
        IGenericRepository<A> ItemA { get; }
        IGenericRepository<B> ItemB { get; }
        IGenericRepository<C> ItemC { get; }
        IGenericRepository<D> ItemD { get; }
        IGenericRepository<E> ItemE { get; }

        Task SaveAsync();
    }

    public interface IGenericRepositoryWrapper<A, B, C, D, E, F>
    {
        IGenericRepository<A, B, C, D, E, F> Item { get; }
        IGenericRepository<A> ItemA { get; }
        IGenericRepository<B> ItemB { get; }
        IGenericRepository<C> ItemC { get; }
        IGenericRepository<D> ItemD { get; }
        IGenericRepository<E> ItemE { get; }
        IGenericRepository<F> ItemF { get; }
        Task SaveAsync();
    }

    public interface IGenericRepositoryWrapper<A, B, C, D, E, F, G>
    {
        IGenericRepository<A, B, C, D, E, F, G> Item { get; }
        IGenericRepository<A> ItemA { get; }
        IGenericRepository<B> ItemB { get; }
        IGenericRepository<C> ItemC { get; }
        IGenericRepository<D> ItemD { get; }
        IGenericRepository<E> ItemE { get; }
        IGenericRepository<F> ItemF { get; }
        IGenericRepository<G> ItemG { get; }
        Task SaveAsync();
    }

    public interface IGenericRepositoryWrapper<A, B, C, D, E, F, G, H>
    {
        IGenericRepository<A, B, C, D, E, F, G, H> Item { get; }
        IGenericRepository<A> ItemA { get; }
        IGenericRepository<B> ItemB { get; }
        IGenericRepository<C> ItemC { get; }
        IGenericRepository<D> ItemD { get; }
        IGenericRepository<E> ItemE { get; }
        IGenericRepository<F> ItemF { get; }
        IGenericRepository<G> ItemG { get; }

        IGenericRepository<H> ItemH { get; }
        Task SaveAsync();
    }

    public interface IGenericRepositoryWrapper<A, B, C, D, E, F, G, H, I>
    {
        IGenericRepository<A, B, C, D, E, F, G, H, I> Item { get; }
        IGenericRepository<A> ItemA { get; }
        IGenericRepository<B> ItemB { get; }
        IGenericRepository<C> ItemC { get; }
        IGenericRepository<D> ItemD { get; }
        IGenericRepository<E> ItemE { get; }
        IGenericRepository<F> ItemF { get; }
        IGenericRepository<G> ItemG { get; }

        IGenericRepository<H> ItemH { get; }

        IGenericRepository<I> ItemI { get; }
        Task SaveAsync();
    }

    public interface IGenericRepositoryWrapper<A, B, C, D, E, F, G, H, I, J>
    {
        IGenericRepository<A, B, C, D, E, F, G, H, I, J> Item { get; }
        IGenericRepository<A> ItemA { get; }
        IGenericRepository<B> ItemB { get; }
        IGenericRepository<C> ItemC { get; }
        IGenericRepository<D> ItemD { get; }
        IGenericRepository<E> ItemE { get; }
        IGenericRepository<F> ItemF { get; }
        IGenericRepository<G> ItemG { get; }

        IGenericRepository<H> ItemH { get; }

        IGenericRepository<I> ItemI { get; }

        IGenericRepository<J> ItemJ { get; }
        Task SaveAsync();
    }

    public interface IGenericRepositoryWrapper<A, B, C, D, E, F, G, H, I, J, K>
    {
        IGenericRepository<A, B, C, D, E, F, G, H, I, J, K> Item { get; }
        IGenericRepository<A> ItemA { get; }
        IGenericRepository<B> ItemB { get; }
        IGenericRepository<C> ItemC { get; }
        IGenericRepository<D> ItemD { get; }
        IGenericRepository<E> ItemE { get; }
        IGenericRepository<F> ItemF { get; }
        IGenericRepository<G> ItemG { get; }

        IGenericRepository<H> ItemH { get; }

        IGenericRepository<I> ItemI { get; }

        IGenericRepository<J> ItemJ { get; }

        IGenericRepository<K> ItemK { get; }
        Task SaveAsync();
    }


    public interface IGenericRepositoryWrapper<A, B, C, D, E, F, G, H, I, J, K, L>
    {
        IGenericRepository<A, B, C, D, E, F, G, H, I, J, K, L> Item { get; }
        IGenericRepository<A> ItemA { get; }
        IGenericRepository<B> ItemB { get; }
        IGenericRepository<C> ItemC { get; }
        IGenericRepository<D> ItemD { get; }
        IGenericRepository<E> ItemE { get; }
        IGenericRepository<F> ItemF { get; }
        IGenericRepository<G> ItemG { get; }

        IGenericRepository<H> ItemH { get; }

        IGenericRepository<I> ItemI { get; }

        IGenericRepository<J> ItemJ { get; }

        IGenericRepository<K> ItemK { get; }

        IGenericRepository<L> ItemL { get; }
        Task SaveAsync();
    }

    public interface IGenericRepositoryWrapper<A, B, C, D, E, F, G, H, I, J, K, L, M>
    {
        IGenericRepository<A, B, C, D, E, F, G, H, I, J, K, L, M> Item { get; }
        IGenericRepository<A> ItemA { get; }
        IGenericRepository<B> ItemB { get; }
        IGenericRepository<C> ItemC { get; }
        IGenericRepository<D> ItemD { get; }
        IGenericRepository<E> ItemE { get; }
        IGenericRepository<F> ItemF { get; }
        IGenericRepository<G> ItemG { get; }

        IGenericRepository<H> ItemH { get; }

        IGenericRepository<I> ItemI { get; }

        IGenericRepository<J> ItemJ { get; }

        IGenericRepository<K> ItemK { get; }

        IGenericRepository<L> ItemL { get; }
        IGenericRepository<M> ItemM { get; }
        Task SaveAsync();
    }

    public interface IGenericRepositoryWrapper<A, B, C, D, E, F, G, H, I, J, K, L, M, N>
    {
        IGenericRepository<A, B, C, D, E, F, G, H, I, J, K, L, M, N> Item { get; }
        IGenericRepository<A> ItemA { get; }
        IGenericRepository<B> ItemB { get; }
        IGenericRepository<C> ItemC { get; }
        IGenericRepository<D> ItemD { get; }
        IGenericRepository<E> ItemE { get; }
        IGenericRepository<F> ItemF { get; }
        IGenericRepository<G> ItemG { get; }

        IGenericRepository<H> ItemH { get; }

        IGenericRepository<I> ItemI { get; }

        IGenericRepository<J> ItemJ { get; }

        IGenericRepository<K> ItemK { get; }

        IGenericRepository<L> ItemL { get; }
        IGenericRepository<M> ItemM { get; }

        IGenericRepository<N> ItemN { get; }
        Task SaveAsync();
    }

    public interface IGenericRepositoryWrapper<A, B, C, D, E, F, G, H, I, J, K, L, M, N, O>
    {
        IGenericRepository<A, B, C, D, E, F, G, H, I, J, K, L, M, N, O> Item { get; }
        IGenericRepository<A> ItemA { get; }
        IGenericRepository<B> ItemB { get; }
        IGenericRepository<C> ItemC { get; }
        IGenericRepository<D> ItemD { get; }
        IGenericRepository<E> ItemE { get; }
        IGenericRepository<F> ItemF { get; }
        IGenericRepository<G> ItemG { get; }

        IGenericRepository<H> ItemH { get; }

        IGenericRepository<I> ItemI { get; }

        IGenericRepository<J> ItemJ { get; }

        IGenericRepository<K> ItemK { get; }

        IGenericRepository<L> ItemL { get; }
        IGenericRepository<M> ItemM { get; }

        IGenericRepository<N> ItemN { get; }
        IGenericRepository<O> ItemO { get; }
        Task SaveAsync();
    }

    public interface IGenericRepositoryWrapper<A, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P>
    {
        IGenericRepository<A, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P> Item { get; }
        IGenericRepository<A> ItemA { get; }
        IGenericRepository<B> ItemB { get; }
        IGenericRepository<C> ItemC { get; }
        IGenericRepository<D> ItemD { get; }
        IGenericRepository<E> ItemE { get; }
        IGenericRepository<F> ItemF { get; }
        IGenericRepository<G> ItemG { get; }

        IGenericRepository<H> ItemH { get; }

        IGenericRepository<I> ItemI { get; }

        IGenericRepository<J> ItemJ { get; }

        IGenericRepository<K> ItemK { get; }

        IGenericRepository<L> ItemL { get; }
        IGenericRepository<M> ItemM { get; }

        IGenericRepository<N> ItemN { get; }
        IGenericRepository<O> ItemO { get; }
        IGenericRepository<P> ItemP { get; }
        Task SaveAsync();
    }

    public interface IGenericRepositoryWrapper<A, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P, Q>
    {
        IGenericRepository<A, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P, Q> Item { get; }
        IGenericRepository<A> ItemA { get; }
        IGenericRepository<B> ItemB { get; }
        IGenericRepository<C> ItemC { get; }
        IGenericRepository<D> ItemD { get; }
        IGenericRepository<E> ItemE { get; }
        IGenericRepository<F> ItemF { get; }
        IGenericRepository<G> ItemG { get; }

        IGenericRepository<H> ItemH { get; }

        IGenericRepository<I> ItemI { get; }

        IGenericRepository<J> ItemJ { get; }

        IGenericRepository<K> ItemK { get; }

        IGenericRepository<L> ItemL { get; }
        IGenericRepository<M> ItemM { get; }

        IGenericRepository<N> ItemN { get; }
        IGenericRepository<O> ItemO { get; }
        IGenericRepository<P> ItemP { get; }
        IGenericRepository<Q> ItemQ { get; }
        Task SaveAsync();
    }

    public interface IGenericRepositoryWrapper<A, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P, Q, R>
    {
        IGenericRepository<A, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P, Q, R> Item { get; }
        IGenericRepository<A> ItemA { get; }
        IGenericRepository<B> ItemB { get; }
        IGenericRepository<C> ItemC { get; }
        IGenericRepository<D> ItemD { get; }
        IGenericRepository<E> ItemE { get; }
        IGenericRepository<F> ItemF { get; }
        IGenericRepository<G> ItemG { get; }

        IGenericRepository<H> ItemH { get; }

        IGenericRepository<I> ItemI { get; }

        IGenericRepository<J> ItemJ { get; }

        IGenericRepository<K> ItemK { get; }

        IGenericRepository<L> ItemL { get; }
        IGenericRepository<M> ItemM { get; }

        IGenericRepository<N> ItemN { get; }
        IGenericRepository<O> ItemO { get; }
        IGenericRepository<P> ItemP { get; }
        IGenericRepository<Q> ItemQ { get; }
        IGenericRepository<R> ItemR { get; }
        Task SaveAsync();
    }
}
