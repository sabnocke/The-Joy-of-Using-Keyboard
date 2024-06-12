(defun ones (dice)
    (reduce #'+ (remove-if-not (lambda (x) (= x 1)) dice )))

(defun twos (dice)
    (reduce #'+ (remove-if-not (lambda (x) (= x 2)) dice )))

(defun threes (dice)
    (reduce #'+ (remove-if-not (lambda (x) (= x 3)) dice )))

(defun fours (dice)
    (reduce #'+ (remove-if-not (lambda (x) (= x 4)) dice )))

(defun fives (dice)
    (reduce #'+ (remove-if-not (lambda (x) (= x 5)) dice )))

(defun sixes (dice)
    (reduce #'+ (remove-if-not (lambda (x) (= x 6)) dice )))

(defun unique (dice)
    (remove-duplicates dice :test #'equal)
)

(defun yacht (dice)
    (if (= (length (unique dice)) 1)
    50
    0))


(defun four-of-a-kind (dice)
    (* (reduce #'+ (remove-if-not (lambda (x) (>= (count x dice) 4)) (unique dice))) 4)
)

(defun little-straight (dice)
    (if (equal NIL (mismatch (sort dice #'<) '(1 2 3 4 5)))
    30
    0)
)

(defun big-straight (dice)
    (if (equal NIL (mismatch (sort dice #'<) '(2 3 4 5 6)))
    30
    0)
)

(defun choice (dice)
    (reduce #'+ dice)
)

(defun full-house (dice)
    (let ((u-dice (unique dice)))
        (if (or
        (> (length (rest u-dice)) 1)
        (/= (logand (count (first u-dice) dice) 2) 2)
        )
    0
    (reduce #'+ dice)
    )
    )
)

(defun score (dice category)
    (cond ((equal category :ones) (ones dice))
          ((equal category :twos) (twos dice))
          ((equal category :threes) (threes dice))
          ((equal category :fours) (fours dice))
          ((equal category :fives) (fives dice))
          ((equal category :sixes) (sixes dice))
          ((equal category :choice) (choice dice))
          ((equal category :full-house) (full-house dice))
          ((equal category :four-of-a-kind) (four-of-a-kind dice))
          ((equal category :little-straight) (little-straight dice))
          ((equal category :big-straight) (big-straight dice))
          ((equal category :yacht) (yacht dice))
    )
)


(print (score '(6 6 4 6 6) :four-of-kind))
(print (score '(3 3 3 3 3) :four-of-kind))
(print (score '(3 3 3 5 5) :four-of-kind))
(print (score '(4 6 2 5 3) :big-straight))