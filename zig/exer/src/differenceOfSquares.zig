pub fn squareOfSum(number: usize) usize {
    // compute the sum of i from 0 to n then square it
    var sum: usize = 0;
    for (1..number + 1) |i| {
        sum += i;
    }
    return sum ** 2;
}

pub fn sumOfSquares(number: usize) usize {
    var sum: usize = 0;
    for (1..number + 1) |i| {
        sum += i * i;
    }
    return sum;
}
pub fn differenceOfSquares(number: usize) usize {
    return squareOfSum(number) - sumOfSquares(number);
}