(ns interest-is-interesting)
(clojure-version)

(import (java.math BigDecimal))

(defn interest-rate
  "Returns the interest rate based on the specified balance."
  [balance]
  (cond
    (< balance 0) -3.213
    (and (>= balance 0) (< balance 1000)) 0.5
    (and (>= balance 1000) (< balance 5000)) 1.621
    (>= balance 5000) 2.475))

(defn annual-balance-update
  "Returns the annual balance update, taking into account the interest rate."
  [balance]
  (if (neg? balance)
    (bigdec (- (* (abs balance) (+ 1 (/ (abs (interest-rate balance)) 100M)))))
    (bigdec (* (abs balance) (+ 1 (/ (abs (interest-rate balance)) 100M))))))

(defn amount-to-donate
  "Returns how much money to donate based on the balance and the tax-free percentage."
  [balance tax-free-percentage]
  (if (pos? balance)
    (int (* (* balance 2) (/ tax-free-percentage 100)))
    0))

(defn annual-balance-update2 [balance]
  (let [interest (interest-rate balance)
        rate-div-100 (/ (BigDecimal. interest) (BigDecimal. "100"))
        abs-balance (BigDecimal. (str (abs balance)))
        new-balance (if (neg? balance)
                      (- abs-balance (* abs-balance rate-div-100))
                      (+ abs-balance (* abs-balance rate-div-100)))]
    new-balance))

(def resultv1 (annual-balance-update 898124017.826243404425M))
(def resultv2 (annual-balance-update2 898124017.826243404425M))
(def expected 920352587.26744292868451875M)
(println "result v1 = " resultv1 (format "\n\tscale = %d, precision = %d" (.scale resultv1) (.precision resultv1)))
(println "result v2 = " resultv2 (format "\n\tscale = %d, precision = %d" (.scale resultv2) (.precision resultv2)))
(println "expected = " expected (format "\n\tscale = %d, precision = %d" (.scale expected) (.precision expected)))
