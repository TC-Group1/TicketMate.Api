﻿using Microsoft.Extensions.DependencyInjection;

namespace TicketMate.Application.Implementation
{
    internal class TypeActivator : ITypeActivator
    {
        private readonly IServiceProvider _serviceProvider;

        public TypeActivator(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

        public TResponse Instantiate<TResponse>(Type typeToInstantiate) => (TResponse)ActivatorUtilities.CreateInstance(_serviceProvider, typeToInstantiate);
    }
}
