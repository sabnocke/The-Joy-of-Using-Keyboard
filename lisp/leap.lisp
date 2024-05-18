(defun leap-year-p (year)
    (evenp (+ (signum (mod year 4)) (signum (mod year 100)) (signum (mod year 400))))
  )

(print (leap-year-p 1997))