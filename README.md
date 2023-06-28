# txtai: Semantic search and workflows for .NET

[![Publish to NUGET](https://github.com/semack/TxtAI.NET/actions/workflows/publish.yml/badge.svg?branch=master)](https://github.com/semack/TxtAI.NET/actions/workflows/publish.yml)
[![Nuget](https://img.shields.io/nuget/v/TxtAI.NET)](https://www.nuget.org/packages/TxtAI.NET)
[![GitHub issues](https://img.shields.io/github/issues-raw/semack/TxtAI.NET)](https://github.com/semack/TxtAI.NET/issues)
[![GitHub last commit (by committer)](https://img.shields.io/github/last-commit/semack/TxtAI.NET)](https://github.com/semack/TxtAI.NET/commits/)

[txtai](https://github.com/neuml/txtai) is an open-source platform for semantic search and workflows powered by language models.

This repository contains .NET bindings for the txtai API. Full txtai functionality is supported.

## Installation

```bash
dotnet add package TxtAI.NET
```

txtai can also be manually built from GitHub.

```bash
git clone https://github.com/semack/TxtAI.NET
cd TxtAI.NET
dotnet build
```

The binaries will be available in ./src/bin

## Examples
The examples directory has a series of examples that give an overview of txtai. See the list of examples below.

| Example     |      Description      |
|:----------|:-------------|
| [Introducing txtai](https://github.com/semack/TxtAI.NET/tree/master/examples/EmbeddingsDemo) | Overview of the functionality provided by txtai |
| [Extractive QA with txtai](https://github.com/semack/TxtAI.NET/tree/master/examples/ExtractorDemo) | Extractive question-answering with txtai |
| [Labeling with zero-shot classification](https://github.com/semack/TxtAI.NET/tree/master/examples/LabelsDemo) | Labeling with zero-shot classification |
| [Pipelines and workflows](https://github.com/semack/TxtAI.NET/tree/master/examples/PipelinesDemo) | Pipelines and workflows |

TxtAI.NET connects to a txtai API instance. See [this link](https://github.com/neuml/txtai#api) for details on how to start a new API instance.

Once an API instance is running, run the examples in a such directory.

