(defclass truck-license (license) ())

(defclass parking () ())
(defclass truck-parking (parking) ())

(defclass vehicle () ())
(defclass automobile (vehicle) ())
(defclass truck (vehicle) ())

(defclass driver () ())
(defmethod license ((driver driver))
  (make-instance 'license))
(defmethod drive ((driver driver) (automobile automobile))
  (format t "~A driving ~A...~%" driver automobile))
(defmethod vehicle ((driver driver))
  (make-instance 'automobile))
(defmethod offer ((driver driver) (parking parking))
  (format t "~A offered a ~A.~%" driver parking))

(defclass truck-driver (driver) ())
(defmethod license ((truck-driver truck-driver))
  (make-instance 'truck-license))
(defmethod drive ((truck-driver truck-driver) (vehicle vehicle))
  (format t "~A driving ~A...~%" truck-driver vehicle))
(defmethod vehicle ((truck-driver truck-driver))
  (if (zerop (random 2))
      (make-instance 'automobile)
    (make-instance 'truck)))
(defmethod offer ((truck-driver truck-driver) (truck-parking truck-parking))
  (format t "~A offered a ~A.~%" truck-driver truck-parking))


(drive (make-instance 'driver) (vehicle (make-instance 'truck-driver)))