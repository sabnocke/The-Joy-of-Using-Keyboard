; g = n * (((45 * pi * d) / 20) + 200)

(defun dough-calculator (pizzas diameter)
    (* (+ (/ (* 45 pi diameter) 20) 200) pizzas)
    )
; d = square-root of ((40 * s) / (3 * pi))
(defun size-from-sauce (sauce)
    (sqrt (/ (* 40 sauce) (* pi 3)))
)

; n = (2 * (l^3)) / (3* pi * (d^2))
(defun pizzas-per-cube (cube-size diameter)
    (floor (/ (* 2 (expt cube-size 3)) (* 3 pi (expt diameter 2))))
)

(defun fair-share-p (pizzas friends)
    (= (mod (* pizzas 8) friends) 0)
)