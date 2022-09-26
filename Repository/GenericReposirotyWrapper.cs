﻿using Contracts;
using Entities;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class GenericRepositoryWrapper<A> : IGenericRepositoryWrapper<A> where A : class
    {
        private readonly RepositoryContext RepositoryContext;

        private IGenericRepository<A> item;
        public IGenericRepository<A> Item 
        { 
            get
            {
                if (item == null)
                    item = new GenericRepository<A>(RepositoryContext);
                return item;
            }
        }

        public GenericRepositoryWrapper(RepositoryContext RepositoryContext)
        {
            this.RepositoryContext = RepositoryContext;
        }

        public async Task SaveAsync()
        {
          await  RepositoryContext.SaveChangesAsync();
        }
    }

    public class GenericRepositoryWrapper<A, B> : IGenericRepositoryWrapper<A, B> where A : class where B : class
    {
        private readonly RepositoryContext RepositoryContext;

        private IGenericRepository<A> itemA;
        public IGenericRepository<A> ItemA
        {
            get
            {
                if (itemA == null)
                    itemA = new GenericRepository<A>(RepositoryContext);
                return itemA;
            }
        }

        private IGenericRepository<A, B> item;
        public IGenericRepository<A, B> Item
        {
            get
            {
                if (item == null)
                    item = new GenericRepository<A, B>(RepositoryContext);
                return item;
            }
        }

        private IGenericRepository<B> itemB;
        public IGenericRepository<B> ItemB
        {
            get
            {
                if (itemB == null)
                    itemB = new GenericRepository<B>(RepositoryContext);
                return itemB;
            }
        }

        public GenericRepositoryWrapper(RepositoryContext RepositoryContext)
        {
            this.RepositoryContext = RepositoryContext;
        }

        public async Task SaveAsync()
        {
            await RepositoryContext.SaveChangesAsync();
        }
    }

    public class GenericRepositoryWrapper<A, B, C> : IGenericRepositoryWrapper<A, B, C> where A : class where B : class where C : class
    {
        private readonly RepositoryContext RepositoryContext;

        private IGenericRepository<A> itemA;
        public IGenericRepository<A> ItemA
        {
            get
            {
                if (itemA == null)
                    itemA = new GenericRepository<A>(RepositoryContext);
                return itemA;
            }
        }

        private IGenericRepository<A, B, C> item;
        public IGenericRepository<A, B, C> Item
        {
            get
            {
                if (item == null)
                    item = new GenericRepository<A, B, C>(RepositoryContext);
                return item;
            }
        }

        private IGenericRepository<B> itemB;
        public IGenericRepository<B> ItemB
        {
            get
            {
                if (itemB == null)
                    itemB = new GenericRepository<B>(RepositoryContext);
                return itemB;
            }
        }

        private IGenericRepository<C> itemC;
        public IGenericRepository<C> ItemC
        {
            get
            {
                if (itemC == null)
                    itemC = new GenericRepository<C>(RepositoryContext);
                return itemC;
            }
        }
        public GenericRepositoryWrapper(RepositoryContext RepositoryContext)
        {
            this.RepositoryContext = RepositoryContext;
        }

        public async Task SaveAsync()
        {
            await RepositoryContext.SaveChangesAsync();
        }
    }

    public class GenericRepositoryWrapper<A, B, C, D> : IGenericRepositoryWrapper<A, B, C, D> where A : class where B : class where C : class where D : class
    {
        private readonly RepositoryContext RepositoryContext;


        private IGenericRepository<A, B, C, D> item;
        public IGenericRepository<A, B, C, D> Item
        {
            get
            {
                if (item == null)
                    item = new GenericRepository<A, B, C, D>(RepositoryContext);
                return item;
            }
        }

        private IGenericRepository<A> itemA;
        public IGenericRepository<A> ItemA
        {
            get
            {
                if (itemA == null)
                    itemA = new GenericRepository<A>(RepositoryContext);
                return itemA;
            }
        }

        private IGenericRepository<B> itemB;
        public IGenericRepository<B> ItemB
        {
            get
            {
                if (itemB == null)
                    itemB = new GenericRepository<B>(RepositoryContext);
                return itemB;
            }
        }

        private IGenericRepository<C> itemC;
        public IGenericRepository<C> ItemC
        {
            get
            {
                if (itemC == null)
                    itemC = new GenericRepository<C>(RepositoryContext);
                return itemC;
            }
        }

        private IGenericRepository<D> itemD;
        public IGenericRepository<D> ItemD
        {
            get
            {
                if (itemD == null)
                    itemD = new GenericRepository<D>(RepositoryContext);
                return itemD;
            }
        }

        public GenericRepositoryWrapper(RepositoryContext RepositoryContext)
        {
            this.RepositoryContext = RepositoryContext;
        }

        public async Task SaveAsync()
        {
            await RepositoryContext.SaveChangesAsync();
        }
    }

    public class GenericRepositoryWrapper<A, B, C, D, E> : IGenericRepositoryWrapper<A, B, C, D, E> where A : class where B : class where C : class where D : class where E : class
    {
        private readonly RepositoryContext RepositoryContext;


        private IGenericRepository<A, B, C, D, E> item;
        public IGenericRepository<A, B, C, D, E> Item
        {
            get
            {
                if (item == null)
                    item = new GenericRepository<A, B, C, D, E>(RepositoryContext);
                return item;
            }
        }

        private IGenericRepository<A> itemA;
        public IGenericRepository<A> ItemA
        {
            get
            {
                if (itemA == null)
                    itemA = new GenericRepository<A>(RepositoryContext);
                return itemA;
            }
        }

        private IGenericRepository<B> itemB;
        public IGenericRepository<B> ItemB
        {
            get
            {
                if (itemB == null)
                    itemB = new GenericRepository<B>(RepositoryContext);
                return itemB;
            }
        }

        private IGenericRepository<C> itemC;
        public IGenericRepository<C> ItemC
        {
            get
            {
                if (itemC == null)
                    itemC = new GenericRepository<C>(RepositoryContext);
                return itemC;
            }
        }

        private IGenericRepository<D> itemD;
        public IGenericRepository<D> ItemD
        {
            get
            {
                if (itemD == null)
                    itemD = new GenericRepository<D>(RepositoryContext);
                return itemD;
            }
        }

        private IGenericRepository<E> itemE;
        public IGenericRepository<E> ItemE
        {
            get
            {
                if (itemE == null)
                    itemE = new GenericRepository<E>(RepositoryContext);
                return itemE;
            }
        }

        public GenericRepositoryWrapper(RepositoryContext RepositoryContext)
        {
            this.RepositoryContext = RepositoryContext;
        }

        public async Task SaveAsync()
        {
            await RepositoryContext.SaveChangesAsync();
        }
    }

    public class GenericRepositoryWrapper<A, B, C, D, E, F> : IGenericRepositoryWrapper<A, B, C, D, E, F> where A : class where B : class where C : class where D : class where E : class where F : class
    {
        private readonly RepositoryContext RepositoryContext;

        private IGenericRepository<A, B, C, D, E, F> item;
        public IGenericRepository<A, B, C, D, E, F> Item
        {
            get
            {
                if (item == null)
                    item = new GenericRepository<A, B, C, D, E, F>(RepositoryContext);
                return item;
            }
        }

        private IGenericRepository<A> itemA;
        public IGenericRepository<A> ItemA
        {
            get
            {
                if (itemA == null)
                    itemA = new GenericRepository<A>(RepositoryContext);
                return itemA;
            }
        }

        private IGenericRepository<B> itemB;
        public IGenericRepository<B> ItemB
        {
            get
            {
                if (itemB == null)
                    itemB = new GenericRepository<B>(RepositoryContext);
                return itemB;
            }
        }

        private IGenericRepository<C> itemC;
        public IGenericRepository<C> ItemC
        {
            get
            {
                if (itemC == null)
                    itemC = new GenericRepository<C>(RepositoryContext);
                return itemC;
            }
        }

        private IGenericRepository<D> itemD;
        public IGenericRepository<D> ItemD
        {
            get
            {
                if (itemD == null)
                    itemD = new GenericRepository<D>(RepositoryContext);
                return itemD;
            }
        }

        private IGenericRepository<E> itemE;
        public IGenericRepository<E> ItemE
        {
            get
            {
                if (itemE == null)
                    itemE = new GenericRepository<E>(RepositoryContext);
                return itemE;
            }
        }

        private IGenericRepository<F> itemF;
        public IGenericRepository<F> ItemF
        {
            get
            {
                if (itemF == null)
                    itemF = new GenericRepository<F>(RepositoryContext);
                return itemF;
            }
        }

        public GenericRepositoryWrapper(RepositoryContext RepositoryContext)
        {
            this.RepositoryContext = RepositoryContext;
        }

        public async Task SaveAsync()
        {
            await RepositoryContext.SaveChangesAsync();
        }
    }

    public class GenericRepositoryWrapper<A, B, C, D, E, F, G> : 
        IGenericRepositoryWrapper<A, B, C, D, E, F, G> where A : class 
        where B : class where C : class where D : class where E : class where F : class where G : class
    {
        private readonly RepositoryContext RepositoryContext;

        private IGenericRepository<A, B, C, D, E, F, G> item;
        public IGenericRepository<A, B, C, D, E, F, G> Item
        {
            get
            {
                if (item == null)
                    item = new GenericRepository<A, B, C, D, E, F, G>(RepositoryContext);
                return item;
            }
        }

        private IGenericRepository<A> itemA;
        public IGenericRepository<A> ItemA
        {
            get
            {
                if (itemA == null)
                    itemA = new GenericRepository<A>(RepositoryContext);
                return itemA;
            }
        }

        private IGenericRepository<B> itemB;
        public IGenericRepository<B> ItemB
        {
            get
            {
                if (itemB == null)
                    itemB = new GenericRepository<B>(RepositoryContext);
                return itemB;
            }
        }

        private IGenericRepository<C> itemC;
        public IGenericRepository<C> ItemC
        {
            get
            {
                if (itemC == null)
                    itemC = new GenericRepository<C>(RepositoryContext);
                return itemC;
            }
        }

        private IGenericRepository<D> itemD;
        public IGenericRepository<D> ItemD
        {
            get
            {
                if (itemD == null)
                    itemD = new GenericRepository<D>(RepositoryContext);
                return itemD;
            }
        }

        private IGenericRepository<E> itemE;
        public IGenericRepository<E> ItemE
        {
            get
            {
                if (itemE == null)
                    itemE = new GenericRepository<E>(RepositoryContext);
                return itemE;
            }
        }

        private IGenericRepository<F> itemF;
        public IGenericRepository<F> ItemF
        {
            get
            {
                if (itemF == null)
                    itemF = new GenericRepository<F>(RepositoryContext);
                return itemF;
            }
        }

        private IGenericRepository<G> itemG;
        public IGenericRepository<G> ItemG
        {
            get
            {
                if (itemG == null)
                    itemG = new GenericRepository<G>(RepositoryContext);
                return itemG;
            }
        }

        public GenericRepositoryWrapper(RepositoryContext RepositoryContext)
        {
            this.RepositoryContext = RepositoryContext;
        }

        public async Task SaveAsync()
        {
            await RepositoryContext.SaveChangesAsync();
        }
    }

    public class GenericRepositoryWrapper<A, B, C, D, E, F, G, H> :
        IGenericRepositoryWrapper<A, B, C, D, E, F, G, H> where A : class
        where B : class where C : class where D : class where E : class where F : class where G : class where H : class
    {
        private readonly RepositoryContext RepositoryContext;

        private IGenericRepository<A, B, C, D, E, F, G, H> item;
        public IGenericRepository<A, B, C, D, E, F, G, H> Item
        {
            get
            {
                if (item == null)
                    item = new GenericRepository<A, B, C, D, E, F, G, H>(RepositoryContext);
                return item;
            }
        }

        private IGenericRepository<A> itemA;
        public IGenericRepository<A> ItemA
        {
            get
            {
                if (itemA == null)
                    itemA = new GenericRepository<A>(RepositoryContext);
                return itemA;
            }
        }

        private IGenericRepository<B> itemB;
        public IGenericRepository<B> ItemB
        {
            get
            {
                if (itemB == null)
                    itemB = new GenericRepository<B>(RepositoryContext);
                return itemB;
            }
        }

        private IGenericRepository<C> itemC;
        public IGenericRepository<C> ItemC
        {
            get
            {
                if (itemC == null)
                    itemC = new GenericRepository<C>(RepositoryContext);
                return itemC;
            }
        }

        private IGenericRepository<D> itemD;
        public IGenericRepository<D> ItemD
        {
            get
            {
                if (itemD == null)
                    itemD = new GenericRepository<D>(RepositoryContext);
                return itemD;
            }
        }

        private IGenericRepository<E> itemE;
        public IGenericRepository<E> ItemE
        {
            get
            {
                if (itemE == null)
                    itemE = new GenericRepository<E>(RepositoryContext);
                return itemE;
            }
        }

        private IGenericRepository<F> itemF;
        public IGenericRepository<F> ItemF
        {
            get
            {
                if (itemF == null)
                    itemF = new GenericRepository<F>(RepositoryContext);
                return itemF;
            }
        }

        private IGenericRepository<G> itemG;
        public IGenericRepository<G> ItemG
        {
            get
            {
                if (itemG == null)
                    itemG = new GenericRepository<G>(RepositoryContext);
                return itemG;
            }
        }

        private IGenericRepository<H> itemH;
        public IGenericRepository<H> ItemH
        {
            get
            {
                if (itemH == null)
                    itemH = new GenericRepository<H>(RepositoryContext);
                return itemH;
            }
        }

        public GenericRepositoryWrapper(RepositoryContext RepositoryContext)
        {
            this.RepositoryContext = RepositoryContext;
        }

        public async Task SaveAsync()
        {
            await RepositoryContext.SaveChangesAsync();
        }
    }

    public class GenericRepositoryWrapper<A, B, C, D, E, F, G, H, I> :
        IGenericRepositoryWrapper<A, B, C, D, E, F, G, H, I> where A : class
        where B : class where C : class where D : class where E : class where F : class where G : class where H : class where I : class
    {
        private readonly RepositoryContext RepositoryContext;

        private IGenericRepository<A, B, C, D, E, F, G, H, I> item;
        public IGenericRepository<A, B, C, D, E, F, G, H, I> Item
        {
            get
            {
                if (item == null)
                    item = new GenericRepository<A, B, C, D, E, F, G, H, I>(RepositoryContext);
                return item;
            }
        }

        private IGenericRepository<A> itemA;
        public IGenericRepository<A> ItemA
        {
            get
            {
                if (itemA == null)
                    itemA = new GenericRepository<A>(RepositoryContext);
                return itemA;
            }
        }

        private IGenericRepository<B> itemB;
        public IGenericRepository<B> ItemB
        {
            get
            {
                if (itemB == null)
                    itemB = new GenericRepository<B>(RepositoryContext);
                return itemB;
            }
        }

        private IGenericRepository<C> itemC;
        public IGenericRepository<C> ItemC
        {
            get
            {
                if (itemC == null)
                    itemC = new GenericRepository<C>(RepositoryContext);
                return itemC;
            }
        }

        private IGenericRepository<D> itemD;
        public IGenericRepository<D> ItemD
        {
            get
            {
                if (itemD == null)
                    itemD = new GenericRepository<D>(RepositoryContext);
                return itemD;
            }
        }

        private IGenericRepository<E> itemE;
        public IGenericRepository<E> ItemE
        {
            get
            {
                if (itemE == null)
                    itemE = new GenericRepository<E>(RepositoryContext);
                return itemE;
            }
        }

        private IGenericRepository<F> itemF;
        public IGenericRepository<F> ItemF
        {
            get
            {
                if (itemF == null)
                    itemF = new GenericRepository<F>(RepositoryContext);
                return itemF;
            }
        }

        private IGenericRepository<G> itemG;
        public IGenericRepository<G> ItemG
        {
            get
            {
                if (itemG == null)
                    itemG = new GenericRepository<G>(RepositoryContext);
                return itemG;
            }
        }

        private IGenericRepository<H> itemH;
        public IGenericRepository<H> ItemH
        {
            get
            {
                if (itemH == null)
                    itemH = new GenericRepository<H>(RepositoryContext);
                return itemH;
            }
        }

        private IGenericRepository<I> itemI;
        public IGenericRepository<I> ItemI
        {
            get
            {
                if (itemI == null)
                    itemI = new GenericRepository<I>(RepositoryContext);
                return itemI;
            }
        }

        public GenericRepositoryWrapper(RepositoryContext RepositoryContext)
        {
            this.RepositoryContext = RepositoryContext;
        }

        public async Task SaveAsync()
        {
            await RepositoryContext.SaveChangesAsync();
        }
    }

    public class GenericRepositoryWrapper<A, B, C, D, E, F, G, H, I, J> :
        IGenericRepositoryWrapper<A, B, C, D, E, F, G, H, I, J> where A : class
        where B : class where C : class where D : class where E : class where F : class where G : class where H : class where I : class where J : class
    {
        private readonly RepositoryContext RepositoryContext;

        private IGenericRepository<A, B, C, D, E, F, G, H, I, J> item;
        public IGenericRepository<A, B, C, D, E, F, G, H, I, J> Item
        {
            get
            {
                if (item == null)
                    item = new GenericRepository<A, B, C, D, E, F, G, H, I, J>(RepositoryContext);
                return item;
            }
        }

        private IGenericRepository<A> itemA;
        public IGenericRepository<A> ItemA
        {
            get
            {
                if (itemA == null)
                    itemA = new GenericRepository<A>(RepositoryContext);
                return itemA;
            }
        }

        private IGenericRepository<B> itemB;
        public IGenericRepository<B> ItemB
        {
            get
            {
                if (itemB == null)
                    itemB = new GenericRepository<B>(RepositoryContext);
                return itemB;
            }
        }

        private IGenericRepository<C> itemC;
        public IGenericRepository<C> ItemC
        {
            get
            {
                if (itemC == null)
                    itemC = new GenericRepository<C>(RepositoryContext);
                return itemC;
            }
        }

        private IGenericRepository<D> itemD;
        public IGenericRepository<D> ItemD
        {
            get
            {
                if (itemD == null)
                    itemD = new GenericRepository<D>(RepositoryContext);
                return itemD;
            }
        }

        private IGenericRepository<E> itemE;
        public IGenericRepository<E> ItemE
        {
            get
            {
                if (itemE == null)
                    itemE = new GenericRepository<E>(RepositoryContext);
                return itemE;
            }
        }

        private IGenericRepository<F> itemF;
        public IGenericRepository<F> ItemF
        {
            get
            {
                if (itemF == null)
                    itemF = new GenericRepository<F>(RepositoryContext);
                return itemF;
            }
        }

        private IGenericRepository<G> itemG;
        public IGenericRepository<G> ItemG
        {
            get
            {
                if (itemG == null)
                    itemG = new GenericRepository<G>(RepositoryContext);
                return itemG;
            }
        }

        private IGenericRepository<H> itemH;
        public IGenericRepository<H> ItemH
        {
            get
            {
                if (itemH == null)
                    itemH = new GenericRepository<H>(RepositoryContext);
                return itemH;
            }
        }

        private IGenericRepository<I> itemI;
        public IGenericRepository<I> ItemI
        {
            get
            {
                if (itemI == null)
                    itemI = new GenericRepository<I>(RepositoryContext);
                return itemI;
            }
        }

        private IGenericRepository<J> itemJ;
        public IGenericRepository<J> ItemJ
        {
            get
            {
                if (itemJ == null)
                    itemJ = new GenericRepository<J>(RepositoryContext);
                return itemJ;
            }
        }

        public GenericRepositoryWrapper(RepositoryContext RepositoryContext)
        {
            this.RepositoryContext = RepositoryContext;
        }

        public async Task SaveAsync()
        {
            await RepositoryContext.SaveChangesAsync();
        }
    }


    public class GenericRepositoryWrapper<A, B, C, D, E, F, G, H, I, J, K> :
        IGenericRepositoryWrapper<A, B, C, D, E, F, G, H, I, J, K> where A : class
        where B : class where C : class where D : class where E : class where F : class where G : class where H : class where I : class where J : class where K : class
    {
        private readonly RepositoryContext RepositoryContext;

        private IGenericRepository<A, B, C, D, E, F, G, H, I, J, K> item;
        public IGenericRepository<A, B, C, D, E, F, G, H, I, J, K> Item
        {
            get
            {
                if (item == null)
                    item = new GenericRepository<A, B, C, D, E, F, G, H, I, J, K>(RepositoryContext);
                return item;
            }
        }

        private IGenericRepository<A> itemA;
        public IGenericRepository<A> ItemA
        {
            get
            {
                if (itemA == null)
                    itemA = new GenericRepository<A>(RepositoryContext);
                return itemA;
            }
        }

        private IGenericRepository<B> itemB;
        public IGenericRepository<B> ItemB
        {
            get
            {
                if (itemB == null)
                    itemB = new GenericRepository<B>(RepositoryContext);
                return itemB;
            }
        }

        private IGenericRepository<C> itemC;
        public IGenericRepository<C> ItemC
        {
            get
            {
                if (itemC == null)
                    itemC = new GenericRepository<C>(RepositoryContext);
                return itemC;
            }
        }

        private IGenericRepository<D> itemD;
        public IGenericRepository<D> ItemD
        {
            get
            {
                if (itemD == null)
                    itemD = new GenericRepository<D>(RepositoryContext);
                return itemD;
            }
        }

        private IGenericRepository<E> itemE;
        public IGenericRepository<E> ItemE
        {
            get
            {
                if (itemE == null)
                    itemE = new GenericRepository<E>(RepositoryContext);
                return itemE;
            }
        }

        private IGenericRepository<F> itemF;
        public IGenericRepository<F> ItemF
        {
            get
            {
                if (itemF == null)
                    itemF = new GenericRepository<F>(RepositoryContext);
                return itemF;
            }
        }

        private IGenericRepository<G> itemG;
        public IGenericRepository<G> ItemG
        {
            get
            {
                if (itemG == null)
                    itemG = new GenericRepository<G>(RepositoryContext);
                return itemG;
            }
        }

        private IGenericRepository<H> itemH;
        public IGenericRepository<H> ItemH
        {
            get
            {
                if (itemH == null)
                    itemH = new GenericRepository<H>(RepositoryContext);
                return itemH;
            }
        }

        private IGenericRepository<I> itemI;
        public IGenericRepository<I> ItemI
        {
            get
            {
                if (itemI == null)
                    itemI = new GenericRepository<I>(RepositoryContext);
                return itemI;
            }
        }

        private IGenericRepository<J> itemJ;
        public IGenericRepository<J> ItemJ
        {
            get
            {
                if (itemJ == null)
                    itemJ = new GenericRepository<J>(RepositoryContext);
                return itemJ;
            }
        }


        private IGenericRepository<K> itemK;
        public IGenericRepository<K> ItemK
        {
            get
            {
                if (itemK == null)
                    itemK = new GenericRepository<K>(RepositoryContext);
                return itemK;
            }
        }

        public GenericRepositoryWrapper(RepositoryContext RepositoryContext)
        {
            this.RepositoryContext = RepositoryContext;
        }

        public async Task SaveAsync()
        {
            await RepositoryContext.SaveChangesAsync();
        }
    }

    public class GenericRepositoryWrapper<A, B, C, D, E, F, G, H, I, J, K, L> :
        IGenericRepositoryWrapper<A, B, C, D, E, F, G, H, I, J, K, L> where A : class
        where B : class where C : class where D : class where E : class where F : class 
        where G : class where H : class where I : class where J : class where K : class where L : class
    {
        private readonly RepositoryContext RepositoryContext;

        private IGenericRepository<A, B, C, D, E, F, G, H, I, J, K, L> item;
        public IGenericRepository<A, B, C, D, E, F, G, H, I, J, K, L> Item
        {
            get
            {
                if (item == null)
                    item = new GenericRepository<A, B, C, D, E, F, G, H, I, J, K, L>(RepositoryContext);
                return item;
            }
        }

        private IGenericRepository<A> itemA;
        public IGenericRepository<A> ItemA
        {
            get
            {
                if (itemA == null)
                    itemA = new GenericRepository<A>(RepositoryContext);
                return itemA;
            }
        }

        private IGenericRepository<B> itemB;
        public IGenericRepository<B> ItemB
        {
            get
            {
                if (itemB == null)
                    itemB = new GenericRepository<B>(RepositoryContext);
                return itemB;
            }
        }

        private IGenericRepository<C> itemC;
        public IGenericRepository<C> ItemC
        {
            get
            {
                if (itemC == null)
                    itemC = new GenericRepository<C>(RepositoryContext);
                return itemC;
            }
        }

        private IGenericRepository<D> itemD;
        public IGenericRepository<D> ItemD
        {
            get
            {
                if (itemD == null)
                    itemD = new GenericRepository<D>(RepositoryContext);
                return itemD;
            }
        }

        private IGenericRepository<E> itemE;
        public IGenericRepository<E> ItemE
        {
            get
            {
                if (itemE == null)
                    itemE = new GenericRepository<E>(RepositoryContext);
                return itemE;
            }
        }

        private IGenericRepository<F> itemF;
        public IGenericRepository<F> ItemF
        {
            get
            {
                if (itemF == null)
                    itemF = new GenericRepository<F>(RepositoryContext);
                return itemF;
            }
        }

        private IGenericRepository<G> itemG;
        public IGenericRepository<G> ItemG
        {
            get
            {
                if (itemG == null)
                    itemG = new GenericRepository<G>(RepositoryContext);
                return itemG;
            }
        }

        private IGenericRepository<H> itemH;
        public IGenericRepository<H> ItemH
        {
            get
            {
                if (itemH == null)
                    itemH = new GenericRepository<H>(RepositoryContext);
                return itemH;
            }
        }

        private IGenericRepository<I> itemI;
        public IGenericRepository<I> ItemI
        {
            get
            {
                if (itemI == null)
                    itemI = new GenericRepository<I>(RepositoryContext);
                return itemI;
            }
        }

        private IGenericRepository<J> itemJ;
        public IGenericRepository<J> ItemJ
        {
            get
            {
                if (itemJ == null)
                    itemJ = new GenericRepository<J>(RepositoryContext);
                return itemJ;
            }
        }


        private IGenericRepository<K> itemK;
        public IGenericRepository<K> ItemK
        {
            get
            {
                if (itemK == null)
                    itemK = new GenericRepository<K>(RepositoryContext);
                return itemK;
            }
        }

        private IGenericRepository<L> itemL;
        public IGenericRepository<L> ItemL
        {
            get
            {
                if (itemL == null)
                    itemL = new GenericRepository<L>(RepositoryContext);
                return itemL;
            }
        }

        public GenericRepositoryWrapper(RepositoryContext RepositoryContext)
        {
            this.RepositoryContext = RepositoryContext;
        }

        public async Task SaveAsync()
        {
            await RepositoryContext.SaveChangesAsync();
        }
    }

    public class GenericRepositoryWrapper<A, B, C, D, E, F, G, H, I, J, K, L, M> :
        IGenericRepositoryWrapper<A, B, C, D, E, F, G, H, I, J, K, L, M> where A : class
        where B : class where C : class where D : class where E : class where F : class
        where G : class where H : class where I : class where J : class where K : class 
        where L : class where M : class
    {
        private readonly RepositoryContext RepositoryContext;

        private IGenericRepository<A, B, C, D, E, F, G, H, I, J, K, L, M> item;
        public IGenericRepository<A, B, C, D, E, F, G, H, I, J, K, L, M> Item
        {
            get
            {
                if (item == null)
                    item = new GenericRepository<A, B, C, D, E, F, G, H, I, J, K, L, M>(RepositoryContext);
                return item;
            }
        }

        private IGenericRepository<A> itemA;
        public IGenericRepository<A> ItemA
        {
            get
            {
                if (itemA == null)
                    itemA = new GenericRepository<A>(RepositoryContext);
                return itemA;
            }
        }

        private IGenericRepository<B> itemB;
        public IGenericRepository<B> ItemB
        {
            get
            {
                if (itemB == null)
                    itemB = new GenericRepository<B>(RepositoryContext);
                return itemB;
            }
        }

        private IGenericRepository<C> itemC;
        public IGenericRepository<C> ItemC
        {
            get
            {
                if (itemC == null)
                    itemC = new GenericRepository<C>(RepositoryContext);
                return itemC;
            }
        }

        private IGenericRepository<D> itemD;
        public IGenericRepository<D> ItemD
        {
            get
            {
                if (itemD == null)
                    itemD = new GenericRepository<D>(RepositoryContext);
                return itemD;
            }
        }

        private IGenericRepository<E> itemE;
        public IGenericRepository<E> ItemE
        {
            get
            {
                if (itemE == null)
                    itemE = new GenericRepository<E>(RepositoryContext);
                return itemE;
            }
        }

        private IGenericRepository<F> itemF;
        public IGenericRepository<F> ItemF
        {
            get
            {
                if (itemF == null)
                    itemF = new GenericRepository<F>(RepositoryContext);
                return itemF;
            }
        }

        private IGenericRepository<G> itemG;
        public IGenericRepository<G> ItemG
        {
            get
            {
                if (itemG == null)
                    itemG = new GenericRepository<G>(RepositoryContext);
                return itemG;
            }
        }

        private IGenericRepository<H> itemH;
        public IGenericRepository<H> ItemH
        {
            get
            {
                if (itemH == null)
                    itemH = new GenericRepository<H>(RepositoryContext);
                return itemH;
            }
        }

        private IGenericRepository<I> itemI;
        public IGenericRepository<I> ItemI
        {
            get
            {
                if (itemI == null)
                    itemI = new GenericRepository<I>(RepositoryContext);
                return itemI;
            }
        }

        private IGenericRepository<J> itemJ;
        public IGenericRepository<J> ItemJ
        {
            get
            {
                if (itemJ == null)
                    itemJ = new GenericRepository<J>(RepositoryContext);
                return itemJ;
            }
        }


        private IGenericRepository<K> itemK;
        public IGenericRepository<K> ItemK
        {
            get
            {
                if (itemK == null)
                    itemK = new GenericRepository<K>(RepositoryContext);
                return itemK;
            }
        }

        private IGenericRepository<L> itemL;
        public IGenericRepository<L> ItemL
        {
            get
            {
                if (itemL == null)
                    itemL = new GenericRepository<L>(RepositoryContext);
                return itemL;
            }
        }

        private IGenericRepository<M> itemM;
        public IGenericRepository<M> ItemM
        {
            get
            {
                if (itemM == null)
                    itemM = new GenericRepository<M>(RepositoryContext);
                return itemM;
            }
        }

        public GenericRepositoryWrapper(RepositoryContext RepositoryContext)
        {
            this.RepositoryContext = RepositoryContext;
        }

        public async Task SaveAsync()
        {
            await RepositoryContext.SaveChangesAsync();
        }
    }

    public class GenericRepositoryWrapper<A, B, C, D, E, F, G, H, I, J, K, L, M, N> :
        IGenericRepositoryWrapper<A, B, C, D, E, F, G, H, I, J, K, L, M, N> where A : class
        where B : class where C : class where D : class where E : class where F : class
        where G : class where H : class where I : class where J : class where K : class
        where L : class where M : class where N : class
    {
        private readonly RepositoryContext RepositoryContext;

        private IGenericRepository<A, B, C, D, E, F, G, H, I, J, K, L, M, N> item;
        public IGenericRepository<A, B, C, D, E, F, G, H, I, J, K, L, M, N> Item
        {
            get
            {
                if (item == null)
                    item = new GenericRepository<A, B, C, D, E, F, G, H, I, J, K, L, M, N>(RepositoryContext);
                return item;
            }
        }

        private IGenericRepository<A> itemA;
        public IGenericRepository<A> ItemA
        {
            get
            {
                if (itemA == null)
                    itemA = new GenericRepository<A>(RepositoryContext);
                return itemA;
            }
        }

        private IGenericRepository<B> itemB;
        public IGenericRepository<B> ItemB
        {
            get
            {
                if (itemB == null)
                    itemB = new GenericRepository<B>(RepositoryContext);
                return itemB;
            }
        }

        private IGenericRepository<C> itemC;
        public IGenericRepository<C> ItemC
        {
            get
            {
                if (itemC == null)
                    itemC = new GenericRepository<C>(RepositoryContext);
                return itemC;
            }
        }

        private IGenericRepository<D> itemD;
        public IGenericRepository<D> ItemD
        {
            get
            {
                if (itemD == null)
                    itemD = new GenericRepository<D>(RepositoryContext);
                return itemD;
            }
        }

        private IGenericRepository<E> itemE;
        public IGenericRepository<E> ItemE
        {
            get
            {
                if (itemE == null)
                    itemE = new GenericRepository<E>(RepositoryContext);
                return itemE;
            }
        }

        private IGenericRepository<F> itemF;
        public IGenericRepository<F> ItemF
        {
            get
            {
                if (itemF == null)
                    itemF = new GenericRepository<F>(RepositoryContext);
                return itemF;
            }
        }

        private IGenericRepository<G> itemG;
        public IGenericRepository<G> ItemG
        {
            get
            {
                if (itemG == null)
                    itemG = new GenericRepository<G>(RepositoryContext);
                return itemG;
            }
        }

        private IGenericRepository<H> itemH;
        public IGenericRepository<H> ItemH
        {
            get
            {
                if (itemH == null)
                    itemH = new GenericRepository<H>(RepositoryContext);
                return itemH;
            }
        }

        private IGenericRepository<I> itemI;
        public IGenericRepository<I> ItemI
        {
            get
            {
                if (itemI == null)
                    itemI = new GenericRepository<I>(RepositoryContext);
                return itemI;
            }
        }

        private IGenericRepository<J> itemJ;
        public IGenericRepository<J> ItemJ
        {
            get
            {
                if (itemJ == null)
                    itemJ = new GenericRepository<J>(RepositoryContext);
                return itemJ;
            }
        }


        private IGenericRepository<K> itemK;
        public IGenericRepository<K> ItemK
        {
            get
            {
                if (itemK == null)
                    itemK = new GenericRepository<K>(RepositoryContext);
                return itemK;
            }
        }

        private IGenericRepository<L> itemL;
        public IGenericRepository<L> ItemL
        {
            get
            {
                if (itemL == null)
                    itemL = new GenericRepository<L>(RepositoryContext);
                return itemL;
            }
        }

        private IGenericRepository<M> itemM;
        public IGenericRepository<M> ItemM
        {
            get
            {
                if (itemM == null)
                    itemM = new GenericRepository<M>(RepositoryContext);
                return itemM;
            }
        }

        private IGenericRepository<N> itemN;
        public IGenericRepository<N> ItemN
        {
            get
            {
                if (itemN == null)
                    itemN = new GenericRepository<N>(RepositoryContext);
                return itemN;
            }
        }

        public GenericRepositoryWrapper(RepositoryContext RepositoryContext)
        {
            this.RepositoryContext = RepositoryContext;
        }

        public async Task SaveAsync()
        {
            await RepositoryContext.SaveChangesAsync();
        }
    }


    public class GenericRepositoryWrapper<A, B, C, D, E, F, G, H, I, J, K, L, M, N, O> :
        IGenericRepositoryWrapper<A, B, C, D, E, F, G, H, I, J, K, L, M, N, O> where A : class
        where B : class where C : class where D : class where E : class where F : class
        where G : class where H : class where I : class where J : class where K : class
        where L : class where M : class where N : class where O : class
    {
        private readonly RepositoryContext RepositoryContext;

        private IGenericRepository<A, B, C, D, E, F, G, H, I, J, K, L, M, N, O> item;
        public IGenericRepository<A, B, C, D, E, F, G, H, I, J, K, L, M, N, O> Item
        {
            get
            {
                if (item == null)
                    item = new GenericRepository<A, B, C, D, E, F, G, H, I, J, K, L, M, N, O>(RepositoryContext);
                return item;
            }
        }

        private IGenericRepository<A> itemA;
        public IGenericRepository<A> ItemA
        {
            get
            {
                if (itemA == null)
                    itemA = new GenericRepository<A>(RepositoryContext);
                return itemA;
            }
        }

        private IGenericRepository<B> itemB;
        public IGenericRepository<B> ItemB
        {
            get
            {
                if (itemB == null)
                    itemB = new GenericRepository<B>(RepositoryContext);
                return itemB;
            }
        }

        private IGenericRepository<C> itemC;
        public IGenericRepository<C> ItemC
        {
            get
            {
                if (itemC == null)
                    itemC = new GenericRepository<C>(RepositoryContext);
                return itemC;
            }
        }

        private IGenericRepository<D> itemD;
        public IGenericRepository<D> ItemD
        {
            get
            {
                if (itemD == null)
                    itemD = new GenericRepository<D>(RepositoryContext);
                return itemD;
            }
        }

        private IGenericRepository<E> itemE;
        public IGenericRepository<E> ItemE
        {
            get
            {
                if (itemE == null)
                    itemE = new GenericRepository<E>(RepositoryContext);
                return itemE;
            }
        }

        private IGenericRepository<F> itemF;
        public IGenericRepository<F> ItemF
        {
            get
            {
                if (itemF == null)
                    itemF = new GenericRepository<F>(RepositoryContext);
                return itemF;
            }
        }

        private IGenericRepository<G> itemG;
        public IGenericRepository<G> ItemG
        {
            get
            {
                if (itemG == null)
                    itemG = new GenericRepository<G>(RepositoryContext);
                return itemG;
            }
        }

        private IGenericRepository<H> itemH;
        public IGenericRepository<H> ItemH
        {
            get
            {
                if (itemH == null)
                    itemH = new GenericRepository<H>(RepositoryContext);
                return itemH;
            }
        }

        private IGenericRepository<I> itemI;
        public IGenericRepository<I> ItemI
        {
            get
            {
                if (itemI == null)
                    itemI = new GenericRepository<I>(RepositoryContext);
                return itemI;
            }
        }

        private IGenericRepository<J> itemJ;
        public IGenericRepository<J> ItemJ
        {
            get
            {
                if (itemJ == null)
                    itemJ = new GenericRepository<J>(RepositoryContext);
                return itemJ;
            }
        }


        private IGenericRepository<K> itemK;
        public IGenericRepository<K> ItemK
        {
            get
            {
                if (itemK == null)
                    itemK = new GenericRepository<K>(RepositoryContext);
                return itemK;
            }
        }

        private IGenericRepository<L> itemL;
        public IGenericRepository<L> ItemL
        {
            get
            {
                if (itemL == null)
                    itemL = new GenericRepository<L>(RepositoryContext);
                return itemL;
            }
        }

        private IGenericRepository<M> itemM;
        public IGenericRepository<M> ItemM
        {
            get
            {
                if (itemM == null)
                    itemM = new GenericRepository<M>(RepositoryContext);
                return itemM;
            }
        }

        private IGenericRepository<N> itemN;
        public IGenericRepository<N> ItemN
        {
            get
            {
                if (itemN == null)
                    itemN = new GenericRepository<N>(RepositoryContext);
                return itemN;
            }
        }

        private IGenericRepository<O> itemO;
        public IGenericRepository<O> ItemO
        {
            get
            {
                if (itemO == null)
                    itemO = new GenericRepository<O>(RepositoryContext);
                return itemO;
            }
        }

        public GenericRepositoryWrapper(RepositoryContext RepositoryContext)
        {
            this.RepositoryContext = RepositoryContext;
        }

        public async Task SaveAsync()
        {
            await RepositoryContext.SaveChangesAsync();
        }
    }

    public class GenericRepositoryWrapper<A, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P> :
        IGenericRepositoryWrapper<A, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P> where A : class
        where B : class where C : class where D : class where E : class where F : class
        where G : class where H : class where I : class where J : class where K : class
        where L : class where M : class where N : class where O : class where P : class
    {
        private readonly RepositoryContext RepositoryContext;

        private IGenericRepository<A, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P> item;
        public IGenericRepository<A, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P> Item
        {
            get
            {
                if (item == null)
                    item = new GenericRepository<A, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P>(RepositoryContext);
                return item;
            }
        }

        private IGenericRepository<A> itemA;
        public IGenericRepository<A> ItemA
        {
            get
            {
                if (itemA == null)
                    itemA = new GenericRepository<A>(RepositoryContext);
                return itemA;
            }
        }

        private IGenericRepository<B> itemB;
        public IGenericRepository<B> ItemB
        {
            get
            {
                if (itemB == null)
                    itemB = new GenericRepository<B>(RepositoryContext);
                return itemB;
            }
        }

        private IGenericRepository<C> itemC;
        public IGenericRepository<C> ItemC
        {
            get
            {
                if (itemC == null)
                    itemC = new GenericRepository<C>(RepositoryContext);
                return itemC;
            }
        }

        private IGenericRepository<D> itemD;
        public IGenericRepository<D> ItemD
        {
            get
            {
                if (itemD == null)
                    itemD = new GenericRepository<D>(RepositoryContext);
                return itemD;
            }
        }

        private IGenericRepository<E> itemE;
        public IGenericRepository<E> ItemE
        {
            get
            {
                if (itemE == null)
                    itemE = new GenericRepository<E>(RepositoryContext);
                return itemE;
            }
        }

        private IGenericRepository<F> itemF;
        public IGenericRepository<F> ItemF
        {
            get
            {
                if (itemF == null)
                    itemF = new GenericRepository<F>(RepositoryContext);
                return itemF;
            }
        }

        private IGenericRepository<G> itemG;
        public IGenericRepository<G> ItemG
        {
            get
            {
                if (itemG == null)
                    itemG = new GenericRepository<G>(RepositoryContext);
                return itemG;
            }
        }

        private IGenericRepository<H> itemH;
        public IGenericRepository<H> ItemH
        {
            get
            {
                if (itemH == null)
                    itemH = new GenericRepository<H>(RepositoryContext);
                return itemH;
            }
        }

        private IGenericRepository<I> itemI;
        public IGenericRepository<I> ItemI
        {
            get
            {
                if (itemI == null)
                    itemI = new GenericRepository<I>(RepositoryContext);
                return itemI;
            }
        }

        private IGenericRepository<J> itemJ;
        public IGenericRepository<J> ItemJ
        {
            get
            {
                if (itemJ == null)
                    itemJ = new GenericRepository<J>(RepositoryContext);
                return itemJ;
            }
        }


        private IGenericRepository<K> itemK;
        public IGenericRepository<K> ItemK
        {
            get
            {
                if (itemK == null)
                    itemK = new GenericRepository<K>(RepositoryContext);
                return itemK;
            }
        }

        private IGenericRepository<L> itemL;
        public IGenericRepository<L> ItemL
        {
            get
            {
                if (itemL == null)
                    itemL = new GenericRepository<L>(RepositoryContext);
                return itemL;
            }
        }

        private IGenericRepository<M> itemM;
        public IGenericRepository<M> ItemM
        {
            get
            {
                if (itemM == null)
                    itemM = new GenericRepository<M>(RepositoryContext);
                return itemM;
            }
        }

        private IGenericRepository<N> itemN;
        public IGenericRepository<N> ItemN
        {
            get
            {
                if (itemN == null)
                    itemN = new GenericRepository<N>(RepositoryContext);
                return itemN;
            }
        }

        private IGenericRepository<O> itemO;
        public IGenericRepository<O> ItemO
        {
            get
            {
                if (itemO == null)
                    itemO = new GenericRepository<O>(RepositoryContext);
                return itemO;
            }
        }

        private IGenericRepository<P> itemP;
        public IGenericRepository<P> ItemP
        {
            get
            {
                if (itemP == null)
                    itemP = new GenericRepository<P>(RepositoryContext);
                return itemP;
            }
        }

        public GenericRepositoryWrapper(RepositoryContext RepositoryContext)
        {
            this.RepositoryContext = RepositoryContext;
        }

        public async Task SaveAsync()
        {
            await RepositoryContext.SaveChangesAsync();
        }
    }

    public class GenericRepositoryWrapper<A, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P, Q> :
        IGenericRepositoryWrapper<A, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P, Q> where A : class
        where B : class where C : class where D : class where E : class where F : class
        where G : class where H : class where I : class where J : class where K : class
        where L : class where M : class where N : class where O : class where P : class
        where Q : class
    {
        private readonly RepositoryContext RepositoryContext;

        private IGenericRepository<A, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P, Q> item;
        public IGenericRepository<A, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P, Q> Item
        {
            get
            {
                if (item == null)
                    item = new GenericRepository<A, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P, Q>(RepositoryContext);
                return item;
            }
        }

        private IGenericRepository<A> itemA;
        public IGenericRepository<A> ItemA
        {
            get
            {
                if (itemA == null)
                    itemA = new GenericRepository<A>(RepositoryContext);
                return itemA;
            }
        }

        private IGenericRepository<B> itemB;
        public IGenericRepository<B> ItemB
        {
            get
            {
                if (itemB == null)
                    itemB = new GenericRepository<B>(RepositoryContext);
                return itemB;
            }
        }

        private IGenericRepository<C> itemC;
        public IGenericRepository<C> ItemC
        {
            get
            {
                if (itemC == null)
                    itemC = new GenericRepository<C>(RepositoryContext);
                return itemC;
            }
        }

        private IGenericRepository<D> itemD;
        public IGenericRepository<D> ItemD
        {
            get
            {
                if (itemD == null)
                    itemD = new GenericRepository<D>(RepositoryContext);
                return itemD;
            }
        }

        private IGenericRepository<E> itemE;
        public IGenericRepository<E> ItemE
        {
            get
            {
                if (itemE == null)
                    itemE = new GenericRepository<E>(RepositoryContext);
                return itemE;
            }
        }

        private IGenericRepository<F> itemF;
        public IGenericRepository<F> ItemF
        {
            get
            {
                if (itemF == null)
                    itemF = new GenericRepository<F>(RepositoryContext);
                return itemF;
            }
        }

        private IGenericRepository<G> itemG;
        public IGenericRepository<G> ItemG
        {
            get
            {
                if (itemG == null)
                    itemG = new GenericRepository<G>(RepositoryContext);
                return itemG;
            }
        }

        private IGenericRepository<H> itemH;
        public IGenericRepository<H> ItemH
        {
            get
            {
                if (itemH == null)
                    itemH = new GenericRepository<H>(RepositoryContext);
                return itemH;
            }
        }

        private IGenericRepository<I> itemI;
        public IGenericRepository<I> ItemI
        {
            get
            {
                if (itemI == null)
                    itemI = new GenericRepository<I>(RepositoryContext);
                return itemI;
            }
        }

        private IGenericRepository<J> itemJ;
        public IGenericRepository<J> ItemJ
        {
            get
            {
                if (itemJ == null)
                    itemJ = new GenericRepository<J>(RepositoryContext);
                return itemJ;
            }
        }


        private IGenericRepository<K> itemK;
        public IGenericRepository<K> ItemK
        {
            get
            {
                if (itemK == null)
                    itemK = new GenericRepository<K>(RepositoryContext);
                return itemK;
            }
        }

        private IGenericRepository<L> itemL;
        public IGenericRepository<L> ItemL
        {
            get
            {
                if (itemL == null)
                    itemL = new GenericRepository<L>(RepositoryContext);
                return itemL;
            }
        }

        private IGenericRepository<M> itemM;
        public IGenericRepository<M> ItemM
        {
            get
            {
                if (itemM == null)
                    itemM = new GenericRepository<M>(RepositoryContext);
                return itemM;
            }
        }

        private IGenericRepository<N> itemN;
        public IGenericRepository<N> ItemN
        {
            get
            {
                if (itemN == null)
                    itemN = new GenericRepository<N>(RepositoryContext);
                return itemN;
            }
        }

        private IGenericRepository<O> itemO;
        public IGenericRepository<O> ItemO
        {
            get
            {
                if (itemO == null)
                    itemO = new GenericRepository<O>(RepositoryContext);
                return itemO;
            }
        }

        private IGenericRepository<P> itemP;
        public IGenericRepository<P> ItemP
        {
            get
            {
                if (itemP == null)
                    itemP = new GenericRepository<P>(RepositoryContext);
                return itemP;
            }
        }


        private IGenericRepository<Q> itemQ;
        public IGenericRepository<Q> ItemQ
        {
            get
            {
                if (itemQ == null)
                    itemQ = new GenericRepository<Q>(RepositoryContext);
                return itemQ;
            }
        }

        public GenericRepositoryWrapper(RepositoryContext RepositoryContext)
        {
            this.RepositoryContext = RepositoryContext;
        }

        public async Task SaveAsync()
        {
            await RepositoryContext.SaveChangesAsync();
        }
    }

    public class GenericRepositoryWrapper<A, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P, Q, R> :
        IGenericRepositoryWrapper<A, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P, Q, R> where A : class
        where B : class where C : class where D : class where E : class where F : class
        where G : class where H : class where I : class where J : class where K : class
        where L : class where M : class where N : class where O : class where P : class
        where Q : class where R : class
    {
        private readonly RepositoryContext RepositoryContext;

        private IGenericRepository<A, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P, Q, R> item;
        public IGenericRepository<A, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P, Q, R> Item
        {
            get
            {
                if (item == null)
                    item = new GenericRepository<A, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P, Q, R>(RepositoryContext);
                return item;
            }
        }

        private IGenericRepository<A> itemA;
        public IGenericRepository<A> ItemA
        {
            get
            {
                if (itemA == null)
                    itemA = new GenericRepository<A>(RepositoryContext);
                return itemA;
            }
        }

        private IGenericRepository<B> itemB;
        public IGenericRepository<B> ItemB
        {
            get
            {
                if (itemB == null)
                    itemB = new GenericRepository<B>(RepositoryContext);
                return itemB;
            }
        }

        private IGenericRepository<C> itemC;
        public IGenericRepository<C> ItemC
        {
            get
            {
                if (itemC == null)
                    itemC = new GenericRepository<C>(RepositoryContext);
                return itemC;
            }
        }

        private IGenericRepository<D> itemD;
        public IGenericRepository<D> ItemD
        {
            get
            {
                if (itemD == null)
                    itemD = new GenericRepository<D>(RepositoryContext);
                return itemD;
            }
        }

        private IGenericRepository<E> itemE;
        public IGenericRepository<E> ItemE
        {
            get
            {
                if (itemE == null)
                    itemE = new GenericRepository<E>(RepositoryContext);
                return itemE;
            }
        }

        private IGenericRepository<F> itemF;
        public IGenericRepository<F> ItemF
        {
            get
            {
                if (itemF == null)
                    itemF = new GenericRepository<F>(RepositoryContext);
                return itemF;
            }
        }

        private IGenericRepository<G> itemG;
        public IGenericRepository<G> ItemG
        {
            get
            {
                if (itemG == null)
                    itemG = new GenericRepository<G>(RepositoryContext);
                return itemG;
            }
        }

        private IGenericRepository<H> itemH;
        public IGenericRepository<H> ItemH
        {
            get
            {
                if (itemH == null)
                    itemH = new GenericRepository<H>(RepositoryContext);
                return itemH;
            }
        }

        private IGenericRepository<I> itemI;
        public IGenericRepository<I> ItemI
        {
            get
            {
                if (itemI == null)
                    itemI = new GenericRepository<I>(RepositoryContext);
                return itemI;
            }
        }

        private IGenericRepository<J> itemJ;
        public IGenericRepository<J> ItemJ
        {
            get
            {
                if (itemJ == null)
                    itemJ = new GenericRepository<J>(RepositoryContext);
                return itemJ;
            }
        }


        private IGenericRepository<K> itemK;
        public IGenericRepository<K> ItemK
        {
            get
            {
                if (itemK == null)
                    itemK = new GenericRepository<K>(RepositoryContext);
                return itemK;
            }
        }

        private IGenericRepository<L> itemL;
        public IGenericRepository<L> ItemL
        {
            get
            {
                if (itemL == null)
                    itemL = new GenericRepository<L>(RepositoryContext);
                return itemL;
            }
        }

        private IGenericRepository<M> itemM;
        public IGenericRepository<M> ItemM
        {
            get
            {
                if (itemM == null)
                    itemM = new GenericRepository<M>(RepositoryContext);
                return itemM;
            }
        }

        private IGenericRepository<N> itemN;
        public IGenericRepository<N> ItemN
        {
            get
            {
                if (itemN == null)
                    itemN = new GenericRepository<N>(RepositoryContext);
                return itemN;
            }
        }

        private IGenericRepository<O> itemO;
        public IGenericRepository<O> ItemO
        {
            get
            {
                if (itemO == null)
                    itemO = new GenericRepository<O>(RepositoryContext);
                return itemO;
            }
        }

        private IGenericRepository<P> itemP;
        public IGenericRepository<P> ItemP
        {
            get
            {
                if (itemP == null)
                    itemP = new GenericRepository<P>(RepositoryContext);
                return itemP;
            }
        }


        private IGenericRepository<Q> itemQ;
        public IGenericRepository<Q> ItemQ
        {
            get
            {
                if (itemQ == null)
                    itemQ = new GenericRepository<Q>(RepositoryContext);
                return itemQ;
            }
        }


        private IGenericRepository<R> itemR;
        public IGenericRepository<R> ItemR
        {
            get
            {
                if (itemR == null)
                    itemR = new GenericRepository<R>(RepositoryContext);
                return itemR;
            }
        }

        public GenericRepositoryWrapper(RepositoryContext RepositoryContext)
        {
            this.RepositoryContext = RepositoryContext;
        }

        public async Task SaveAsync()
        {
            await RepositoryContext.SaveChangesAsync();
        }
    }
}
