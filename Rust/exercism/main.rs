use std::collections::HashSet;
use std::iter::FromIterator;

fn main() {
    println!("score: {}", score([6, 6, 4, 6, 6], Category::FourOfAKind));
    println!(
        "LittleStraight score: {}",
        score([3, 3, 3, 3, 3], Category::FourOfAKind)
    );
}

type Dice = [u8; 5];

pub enum Category {
    Ones,
    Twos,
    Threes,
    Fours,
    Fives,
    Sixes,
    FullHouse,
    FourOfAKind,
    LittleStraight,
    BigStraight,
    Choice,
    Yacht,
}

fn unique(coll: &Dice) -> Vec<u8> {
    let mut uset = HashSet::new();
    for &item in coll.iter() {
        uset.insert(item);
    }
    uset.into_iter().collect()
}

pub fn score(dice: Dice, category: Category) -> u8 {
    let decision = match category {
        Category::Ones => counter(dice, 1),
        Category::Twos => counter(dice, 2) * 2,
        Category::Threes => counter(dice, 3) * 3,
        Category::Fours => counter(dice, 4) * 4,
        Category::Fives => counter(dice, 5) * 5,
        Category::Sixes => counter(dice, 6) * 6,
        Category::FourOfAKind => four_of_kind(dice),
        Category::FullHouse => full_house(dice),
        Category::LittleStraight => mismatch(dice, vec![1, 2, 3, 4, 5]),
        Category::BigStraight => mismatch(dice, vec![2, 3, 4, 5, 6]),
        Category::Choice => dice.iter().sum(),
        Category::Yacht => yacht(dice),
    };
    return decision;
}

fn counter(dice: Dice, digit: u8) -> u8 {
    dice.iter().filter(|&x| *x == digit).count() as u8
}

fn full_house(dice: Dice) -> u8 {
    let uniq = unique(&dice);
    if uniq.len() > 2 {
        return 0;
    }
    if (counter(dice, uniq[0]) & 2) != 2 {
        return 0;
    }
    return dice.iter().sum();
}

fn four_of_kind(dice: Dice) -> u8 {
    let uniq = unique(&dice);
    if uniq.len() > 2 {
        return 0;
    }
    let first = counter(dice, uniq[0]);
    if first >= 4 {
        return uniq[0] * 4;
    } else {
        return uniq[1] * 4;
    }
}

fn mismatch(dice: Dice, other: Vec<u8>) -> u8 {
    let mask: HashSet<u8> = HashSet::from_iter(other);
    let proc: HashSet<u8> = HashSet::from_iter(dice);
    if proc.intersection(&mask).count() == 5 {
        return 30;
    }
    return 0;
}

fn yacht(dice: Dice) -> u8 {
    if unique(&dice).len() != 1 {
        return 0;
    }
    return 50;
}
