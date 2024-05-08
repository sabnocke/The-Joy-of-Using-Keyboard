const ComputationError = error {
    IllegalArgument
};

pub fn steps(n: usize) ComputationError!usize {
    if(n == 0) return ComputationError.IllegalArgument;

    var step: usize = 0;
    var num = n;
    while (num != 1) : (step += 1) {
        if (num % 2 == 0) {
            num >>= 1;
        } else {
            num = 3 * num + 1;
        }
    }
    return step;
}