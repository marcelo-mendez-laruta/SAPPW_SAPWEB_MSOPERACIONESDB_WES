using BCP.Framework.Logs;
using BCP.Sap.Authentication;
using BCP.Sap.Business;
using BCP.Sap.Models.OperacionesDB;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace BCP.Sap.Microservicio.OperacionesDB
{
    public class Startup
    {
        readonly string dominiosPermitidos = "dominiosPermitidos";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            #region SECCION.01: CONFIGURACION DE SERVICIO
            //Se realiza la configuracion del servicio por medio de inyeccion de dependencias.
            var miConfiguracion = this.Configuration.GetSection("aplicacion").Get<OperacionesDBConfig>();
            services.AddSingleton<ILogger, Logger>(objeto => new Logger(miConfiguracion.configuracionLog.rutaArchivoLog, miConfiguracion.configuracionLog.nivel));
            services.AddSingleton< IBusinessOperacionesDB,BusinessOperacionesDB > (objeto => new BusinessOperacionesDB(miConfiguracion));
            #endregion

            #region SECCION.02: CONFIGURACION AUTORIZACION BASICA
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.OperationFilter<ManagerHeaderFilter>();

                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = miConfiguracion.configuracionAplicacion.nombre,
                        Version = "v1",
                        Description = miConfiguracion.configuracionAplicacion.descripcion,
                        Contact = new OpenApiContact
                        {
                            Name = miConfiguracion.configuracionAplicacion.equipoDesarrollo,
                            Email = miConfiguracion.configuracionAplicacion.equipoDesarrolloContacto,
                            Url = new Uri("http://intranetbcp/"),
                        }
                    });
                string rutaModelos = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "BCP.Sap.Models.xml");
                c.IncludeXmlComments(rutaModelos);
                string nombreProyecto = Assembly.GetExecutingAssembly().GetName().Name;
                string rutaMetodos = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), nombreProyecto + ".xml");
                c.IncludeXmlComments(rutaMetodos);
                c.AddSecurityDefinition("basic", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "basic",
                    In = ParameterLocation.Header,
                    Description = miConfiguracion.configuracionAplicacion.solicitudAutorizacion
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                     {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "basic"
                                }
                            },
                            new string[] {}
                    }
                     });
            });

            #endregion

            #region SECCION.03: CONFIGURACION CORS
            string[] origenes = miConfiguracion.configuracionAplicacion.origenesPermitidos.Split(';');
            services.AddCors(options =>
            {
                options.AddPolicy(dominiosPermitidos,
                builder =>
                {
                    builder.WithOrigins(origenes)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
                });
            });
            #endregion

            #region SECCION.04: ESQUEMA DE AUTORIZACION
            services.AddAuthentication("BasicAuthentication").AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);
            services.AddScoped<IManagerUserService, ManagerUserService>(s => new ManagerUserService(miConfiguracion));
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                string swaggerJsonBasePath = string.IsNullOrWhiteSpace(c.RoutePrefix) ? "." : "..";
                c.SwaggerEndpoint($"{swaggerJsonBasePath}/swagger/v1/swagger.json", "V1");
            });

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCors(this.dominiosPermitidos);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
