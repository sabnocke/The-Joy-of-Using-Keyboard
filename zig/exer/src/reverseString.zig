/// Writes a reversed copy of `s` to `buffer`.
pub fn reverse(buffer: []u8, s: []const u8) []u8 {
    const s_len: usize = s.len;
    for (0..s_len) |i| {
        buffer[i] = s[s_len - i - 1];
    }
    return buffer;
}