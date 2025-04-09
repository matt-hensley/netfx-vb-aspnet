Default .NET Framework/VB.Net web app template with these packages setup:
* OpenTelemetry.Exporter.OpenTelemetryProtocol
* OpenTelemetry.Instrumentation.AspNet
* OpenTelemetry.Instrumentation.Http
* OpenTelemetry.Instrumentation.Owin

To fully test locally, consider using https://github.com/grafana/docker-otel-lgtm/

The run-lgtm scripts will fire up a complete telemetry backend suitable for development and testing, including a collector instance running on default ports
