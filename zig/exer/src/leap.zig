pub fn leap_year(year: u32) bool {
    const count: u32 = sign(year % 4) + sign(year % 100) + sign(year % 400);
    return count % 2 == 0;
}

fn sign(n: u32) u32 {
    if (n > 0) return 1;
    return 0;
}
