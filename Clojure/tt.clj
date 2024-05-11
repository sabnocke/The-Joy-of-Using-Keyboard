(ns tt)

(defn average
  [input]
  (/ (reduce + input) (count input))
  )

(println (average '(1)))