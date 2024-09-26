FROM mcr.microsoft.com/dotnet/sdk:8.0 AS base
WORKDIR /app

RUN apt-get update && \
    apt-get install -y --no-install-recommends python3 python3-pip python3-venv && \
    apt-get clean && rm -rf /var/lib/apt/lists/*

RUN python3 -m venv /app/venv

RUN /app/venv/bin/pip install --upgrade pip && \
    /app/venv/bin/pip install transformers torch huggingface_hub accelerate && \
    rm -rf ~/.cache/pip

COPY ["LlamaExample.csproj", "./"]
RUN dotnet restore "LlamaExample.csproj"

COPY . .
RUN dotnet build "LlamaExample.csproj" -c Release -o /app/build

RUN dotnet publish "LlamaExample.csproj" -c Release -o /app/publish /p:UseAppHost=false

COPY llama_script.py /app/publish/

WORKDIR /app/publish

ENV PATH="/app/venv/bin:$PATH"

ENTRYPOINT ["dotnet", "LlamaExample.dll"]