type
  Allergen* = enum
    Eggs, Peanuts, Shellfish, Strawberries, Tomatoes, Chocolate, Pollen, Cats

proc isAllergicTo*(score: int, allergen: Allergen): bool =
  let index: int = 1 shl int(allergen)
  echo index
  return (score and index) >= 1

proc allergies*(score: int): set[Allergen] =
  var s: set[Allergen] = {}
  for allergen in Allergen:
    if(isAllergicTo(score, allergen)):
      incl(s, allergen)
  return s

echo isAllergicTo(129, Eggs)
echo allergies(129)