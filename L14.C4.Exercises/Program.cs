using System.Globalization;
using L14.C4.Exercises;
using Option;
using static System.Console;
using static L14.C4.Exercises.C4ExerciseExtensions;
// Repetition is the mother of skill
// ref | in | out
int loh = 4;
Do(ref loh); // gets address in memory
WriteLine(loh); // 8

int poh = 33;
Do2(in poh); // readonly arg
WriteLine(poh); // return new value, the original is the same = 33

Do3(in poh, out var koh);
WriteLine(koh); // return new value and initializes it, the original is the same = 33

/* Ex.1 Implement Map for ISet<T> and IDictionary<K, T>. (Tip: start by writing down
the signature in arrow notation.) */
Dictionary<int, double> dict = new Dictionary<int, double>()
{
    { 1, 1 },
    { 2, 3 },
    { 3, 6 },
    { 4, 12 },
    { 5, 24 },
    { 6, 48 }
};

var dict2 = dict.Map(value 
        => value.ToString(CultureInfo.InvariantCulture)); // => IDictionary<int, string>...
unchecked // just because I want
{
                                                            // 104
    HashSet<byte> set = [1, 3, 5, 10, 15, 30, 45, 90, 180, (byte)360];
    var set2 = set.Map(value => (int)value);
}

// Ex.2 Implement Map for Option and IEnumerable in terms of Bind and Return.
var some = F.Some("hello").ToOption();
var other = some.Map(o => 1);

var many = Enumerable.Range(1, 39);
var others = many.Map(o => o.ToString());

/* Ex.3 Use Bind and an Option-returning Lookup function (such as the one we defined
in chapter 3) to implement GetWorkPermit, shown below. Then enrich the
    implementation so that GetWorkPermit returns None if the work permit has
expired. */
var employees = new List<Employee>
{
    new Employee
    {
        JoinedOn = new DateTime(2015, 5, 1),
        LeftOn = new Some<DateTime>(new DateTime(2020, 5, 1)),
        WorkPermit = new Some<WorkPermit>(
            new WorkPermit
            {
                Expiry = DateTime.UtcNow.AddYears(1)
            })
    },
    new Employee
    {
        JoinedOn = new DateTime(2018, 3, 15),
        LeftOn = new None(),
        WorkPermit = new Some<WorkPermit>(
            new WorkPermit
            {
                Expiry = DateTime.UtcNow.AddMonths(-1)
            })
    },
    new Employee
    {
        JoinedOn = new DateTime(2020, 1, 10),
        LeftOn = new None(),
        WorkPermit = new None()
    }
};

var people = new Dictionary<string, Employee>
{
    ["emp1"] = employees[0],
    ["emp2"] = employees[1],
    ["emp3"] = employees[2]
};

// Проверка AverageYearsWorkedAtTheCompany
double avgYears = AverageYearsWorkedAtTheCompany(employees);
double avg = AverageYearsWorkedAtTheCompanyV2(employees);
WriteLine($"Среднее количество лет работы: {avgYears:F2}");

// Проверка GetWorkPermit
var permit1 = GetWorkPermit(people, "emp1");
var permit2 = GetWorkPermit(people, "emp2");
var permit3 = GetWorkPermit(people, "emp3");

WriteLine("emp1 permit: " + permit1.Match(
    none: () => "нет или просрочено",
    some: wp => $"действительно до {wp.Expiry:d}"
));

WriteLine("emp2 permit: " + permit2.Match(
    none: () => "нет или просрочено",
    some: wp => $"действительно до {wp.Expiry:d}"
));

WriteLine("emp3 permit: " + permit3.Match(
    none: () => "нет или просрочено",
    some: wp => $"действительно до {wp.Expiry:d}"
));

ReadKey();


void Do(ref int n) => n *= 2;

int Do2(in int n) => n * 2;

void Do3(in int a, out int b) => b = a * 3;