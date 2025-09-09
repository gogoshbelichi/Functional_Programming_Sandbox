using L12.C4.Patterns_in_FP;
using Option;
using static L12.C4.Patterns_in_FP.NewExtensions;
using static System.Console;

// Mapping a function onto a sequence
Enumerable.Range(1, 3).Map(Times3); // => [3, 6, 9]

// Mapping function onto Option
Option<string> none = None.Default;
Option<string> optJohn = new Some<string>("John");
var x = none.Map(Greet); // => None 
var y = optJohn.Map(Greet); // => Some("hello, John")

Option<Apple> full  = new Some<Apple>(new Apple());
Option<Apple> empty = None.Default;
var a = full.Map(MakePie);  // => Some(ApplePie) 
var b = empty.Map(MakePie); // => None

Option<Subject> subj = new Some<Subject>(new Subject() { Age = new Age(21), Gender = Gender.Female});
var r = subj.Map(RiskOf); // => Low

ReadLine();

