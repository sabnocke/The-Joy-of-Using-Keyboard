(module
  ;;
  ;; Calculate the square of the sum of the first N natural numbers
  ;;
  ;; @param {i32} max - The upper bound (inclusive) of natural numbers to consider
  ;;
  ;; @returns {i32} The square of the sum of the first N natural numbers
  ;;

    (func $square (export "square") (param $n i32) (result i32)
        (i32.mul
        (local.get $n)
        (local.get $n)
        )
    )

  (func $squareOfSum (export "squareOfSum") (param $max i32) (result i32)
    (local $sum i32)
    (local $upper i32)
    (local.set $upper (i32.mul (local.get $max) (i32.add (i32.const 1) (local.get $max))))
    (local.set $sum (i32.div_u (local.get $upper) (i32.const 2)))
    (local.set $sum (call $square (local.get $sum)))
    (local.get $sum)
  )

  ;;
  ;; Calculate the sum of the squares of the first N natural numbers
  ;;
  ;; @param {i32} max - The upper bound (inclusive) of natural numbers to consider
  ;;
  ;; @returns {i32} The sum of the squares of the first N natural numbers
  ;;
  (func $sumOfSquares (export "sumOfSquares") (param $max i32) (result i32)
    (local $sum i32)
    (local $i i32)
    (local.set $sum (i32.const 0))
    (local.set $i (i32.const 0))
    (block $end (loop $loop
        (if (i32.gt_u (local.get $i) (local.get $max))
            (then
                (br $end) ;; end if i >= max
            )
        )
        (local.set $sum (i32.add (local.get $sum) (call $square (local.get $i))))
        (local.set $i (i32.add (local.get $i) (i32.const 1)))
        (br $loop)
    ))
    (local.get $sum)
  )

  ;;
  ;; Calculate the difference between the square of the sum and the sum of the
  ;; squares of the first N natural numbers.
  ;;
  ;; @param {i32} max - The upper bound (inclusive) of natural numbers to consider
  ;;
  ;; @returns {i32} Difference between the square of the sum and the sum of the
  ;;                squares of the first N natural numbers.
  ;;
  (func $difference (export "difference") (param $max i32) (result i32)
    (i32.sub
        (call $squareOfSum (local.get $max))
        (call $sumOfSquares (local.get $max))
    )
  )
  (func $_start (export "_start") (result i32)
    ;; (call $sumOfSquares (i32.const 10))
    (call $difference (i32.const 10))
  )
)


