using IPB2.IncompatibleFoodApi.Database.AppDbContextModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using PB2.IncompatibleFood.Domain.Features.IncompatibleFood;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PB2.IncompatibleFood.Domain;

public static class FeatureManager
{
    public static WebApplicationBuilder AddDomain(this WebApplicationBuilder builder)
    {
        // Features
        builder.Services.AddScoped<IncompatibleFoodService>();

        return builder;
    }

    public static WebApplicationBuilder AddDatabase(this WebApplicationBuilder builder)
    {
        // DbContext
        builder.Services.AddDbContext<AppDbContext>();

        return builder;
    }
}
