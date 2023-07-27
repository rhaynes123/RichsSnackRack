// See https://aka.ms/new-console-template for more information
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Microsoft.EntityFrameworkCore;
using RichsSnackRack.Menu;
using RichsSnackRack.PerformanceTests.Menu.QueryTests;
using RichsSnackRack.Persistence;
using Xunit;

//Console.WriteLine("Hello, World!");
BenchmarkSwitcher.FromAssembly(assembly: typeof(GetAllMenuQueryTests).Assembly).Run(args);

//var summary = BenchmarkRunner.Run<GetAllMenuQueryTests>();

