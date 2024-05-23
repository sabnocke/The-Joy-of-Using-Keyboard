const pow = @import("std").math.pow;

pub const Coordinate = struct {
    x: f32,
    y: f32,

    pub fn init(x_coord: f32, y_coord: f32) Coordinate {
        return Coordinate{ .x = x_coord, .y = y_coord };
    }
    pub fn score(self: Coordinate) usize {
        const norm = @sqrt(pow(f32, self.x, 2) + pow(f32, self.y, 2));
        if (0 <= norm and norm <= 1) return 10;
        if (1 < norm and norm <= 5) return 5;
        if (5 < norm and norm <= 10) return 1;
        return 0;
    }
};
