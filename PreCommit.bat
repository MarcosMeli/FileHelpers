Build\VersionAdder Build\CurrentVersion.msbuild "</CurrentVersion>" 
@%windir%\Microsoft.NET\Framework\v4.0.30319\msbuild Build\FileHelpers.msbuild /t:version /tv:4.0 /nologo
