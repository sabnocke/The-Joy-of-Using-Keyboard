(defun within-p (first select last)
(and (<= first select) (<= select last))
)


(defun pal-picker (personality)
  (case personality
    (:lazy "Cat")
    (:energetic "Dog")
    (:quiet "Fist")
    (:hungry "Rabbit")
    (:talkative "Bird")
    (otherwise "I don't know... A dragon?"))
  )

; More than or equal to 40kg -> :massive
; 20kg to 39kg inclusive -> :large
; 10kg to 19kg inclusive -> :medium
; 1kg to 9kg inclusive -> :small
; Less than or equal to 0kg -> :just-your-imagination


(defun habitat-fitter (weight)
(cond ((>= weight 40) :massive)
      ((within-p 20 weight 39) :large)
      ((within-p 10 weight 19) :medium)
      ((within-p 1 weight 9) :small)
      (t :just-your-imagination))
)

(defun feeding-time-p (fullness)
    (if (>= fullness 20)
        "All is well."
        "It's feeding time!")
)

(defun pet (pet)
    (if (string= pet "Fish")
        "Maybe not with this pet...")
)

(defun play-fetch (pet)
(unless (string= pet "Dog")
    "Maybe not with this pet...")
)

(print (<= 20 30))
(print (<= 10 20))
(print (within-p 10 20 30))