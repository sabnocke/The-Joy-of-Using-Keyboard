
(defun convert (n)
  "Converts a number to a string of raindrop sounds."
  (cond ((= (mod n 105) 0) "PlingPlangPlong") ; 3 5 7
        ((= (mod n 35) 0) "PlangPlong")
        ((= (mod n 21) 0) "PlingPlong")
        ((= (mod n 15) 0) "PlingPlang")
        ((= (mod n 7) 0) "Plong")
        ((= (mod n 5) 0) "Plang")
        ((= (mod n 3) 0) "Pling")
        (t (write-to-string n))
        )
        )

(print (convert 30))
