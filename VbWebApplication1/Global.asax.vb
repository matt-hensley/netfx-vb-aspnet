Imports System.Web.Http
Imports System.Web.Optimization
Imports OpenTelemetry
Imports OpenTelemetry.Metrics
Imports OpenTelemetry.Trace

Public Class WebApiApplication
    Inherits System.Web.HttpApplication

    Private meterProvider As MeterProvider
    Private traceProvider As TracerProvider

    Sub Application_Start()
        ' ASP.NET instrumentation requires manual web.config changes
        ' https://github.com/open-telemetry/opentelemetry-dotnet-contrib/tree/main/src/OpenTelemetry.Instrumentation.AspNet
        ' OWIN instrumentation requires manual pipeline changes if self-hosted
        ' https://github.com/open-telemetry/opentelemetry-dotnet-contrib/tree/main/src/OpenTelemetry.Instrumentation.Owin
        meterProvider = Sdk.CreateMeterProviderBuilder() _
            .AddAspNetInstrumentation() _
            .AddHttpClientInstrumentation() _
            .AddOwinInstrumentation() _
            .AddOtlpExporter() _
            .Build()
        traceProvider = Sdk.CreateTracerProviderBuilder() _
            .AddAspNetInstrumentation() _
            .AddHttpClientInstrumentation() _
            .AddOwinInstrumentation() _
            .AddOtlpExporter() _
            .Build()

        AreaRegistration.RegisterAllAreas()
        GlobalConfiguration.Configure(AddressOf WebApiConfig.Register)
        FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters)
        RouteConfig.RegisterRoutes(RouteTable.Routes)
        BundleConfig.RegisterBundles(BundleTable.Bundles)
    End Sub

    Sub Application_End()
        ' Disposing providers causes the SDK to flush all telemetry
        meterProvider?.Dispose()
        traceProvider?.Dispose()
    End Sub
End Class
