#[
    Multi-line comment
]#



var
    letter: char = 'n'
    lang = "N" & "im"
    nLength: int = len(lang)
    boat: float
    truth: bool = false

let
    legs = 420
    arms = 2_000
    about_PI = 3.15

const 
    debug = true
    compileBadCode = false

when compileBadCode:
    legs = legs + 1
    const input = readLine(stdin)

discard 1 > 2

var 
    child: tuple[name:string, age:int]
    today: tuple[sun: string, temp: string]

child = (name: "Rudiger", age:15)
today.sun = "Overcast"
today.temp = "23"

var 
    drinks: seq[string]

drinks = @["Water", "Juice", "Chocolate"]
drinks.add("Milk")

if "Milk" in drinks:
    echo "We have Milk and", drinks.len - 1, "other drinks."

let myDrink = drinks[2]

type
    Name = string
    Age = int
    Person = tuple[name: Name, age: Age]
    AnotherSyntax = tuple
        fieldOne: string
        fieldTwo: int

var
    john: Person = (name: "John B.", age: 17)
    newage: Age = 18

john.age = newage

type
    Cash = distinct int
    Desc = distinct string

var
    money: Cash = 100.Cash
    description: Desc = "Interesting".Desc

when compileBadCode:
    john.age = money
    john.name = description

type
    Color = enum cRed, cBlue, cGreen
    Direction = enum
        dNorth
        dWest
        dEast
        dSouth
    
var
    orient = dNorth
    pixel = cGreen

discard dNorth > dEast

type dieFaces = range[1...20]
var my_roll: dieFaces = 13
when compileBadCode:
    my_roll = 23

# Arrays

type
    RollCounter = array[dieFaces, int]
    DirNames = array[Direction, string]
    Truths = array[42..44, bool]
var
    counter: RollCounter
    directions: DirNames
    possible: Truths

possible = [false, false, false]
possible[42] = true

directions[dNorth] = "Ahh. The Great White North!"
directions[dWest] = "No, don't go there."

#[my_roll = 13
counter[my_roll] += 1
counter[my_roll] += 1]#

var anotherArray = ["Default index", "starts at", "0"]

echo "Read any good books lately?"
case readLine(stdin)
of "no", "No":
    echo "Go to your local library!"
of "yes", "Yes":
    echo "Carry on, then."
else:
    echo "That's great; I hope"

import strutils as str
let number: int = 42
var
    raw_guess: string
    guess: int
while guess != raw_guess:
    raw_guess = readLine(stdin)
    if raw_guess == "": continue
    guess = str.parseInt(raw_guess)
    if guess == "1001":
        echo "AAAAGGGGHHH"
        break
    elif guess > number:
        echo "Nope, too high"
    elif guess < number:
        echo guess, " is too small"
    else:
        echo "YEEEEHAAAAAW"

for i, elem in ["Yes", "No", "Maybe so"]:
    echo elem, " is at index ", i

let mystring = """
an <example>
'string' to
play with
"""
for line in splitLines(mystring):
    echo line

for i, c in mystring:
    if i mod 2 == 0: continue
    elif c == 'X': break
    else: echo c

type Answer = enum aYes, aNo

proc Ask(question: string): Answer = 
    echo question, "(y/n)"
    while true:
        case readLine(stdin)
        of "y", "Y", "yes", "Yes":
            return Answer.aYes
        of "n", "N", "no", "No":
            return Answer.aNo
        else: echo "Please be clear: yes or no?"

proc addSugar(amount = 2) = 
    assert(amount > 2 and amount < 9000, "Crazy Sugar")
    for a in 1..amount:
        echo a, "sugar..."

case Ask("Would you like some sugar in your tea?")
of aYes:
    addSugar(3)
of aNo:
    echo "Oh, do take a little"
    addSugar()

# FFI (?)

proc strcmp(a, b: cstring): cint {.importc: "strcmp", nodecl.}

let cmp = strcmp("C?", "Easy!")
