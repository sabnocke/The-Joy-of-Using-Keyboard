(ns fiel)

(def t1 '(1 3 5 7 9))

(defn average
  [input]
  (cond
    (> (count input) 0) (float (/ (reduce + input) (count input)))
    :else 0
    )
  )

(defn drop-nth [n coll]
  (lazy-seq
    (when-let [s (seq coll)]
      (concat (take (dec n) (rest s))
              (drop-nth n (drop n s))
              )
      )
    )
  )


(defn average-approx?
  [input]
  (or
    (= (float (/ (+ (first input) (last input)) 2))
      (average input))
    (=
      (float (nth input (/ (count input) 2)))
      (average input))
      )
  )



(defn double-last-cond
  [hand]
  (concat
    (take (dec (count hand)) hand)
    (let [val (last hand)]
      (if (= val 11)
        [(* val 2)]
        [val])
    )
  ))

(defn evnodd
  [hand]
  (=
    (average (take-nth 2 hand))
    (average (drop-nth 2 hand))
    )
  )



(println (evnodd t1))
(println (empty? (filter #(= % 8) t1)))
(println (not (nil? (some #{5} t1))))