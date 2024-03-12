# import stuff
import matplotlib.pyplot as plt
import numpy as np
import tensorflow as tf

print(f"{tf.__version__=}")
true: bool = True
false: bool = False
class_names = ['T-shirt/top', 'Trouser', 'Pullover', 'Dress', 'Coat',
               'Sandal', 'Shirt', 'Sneaker', 'Bag', 'Ankle boot']


def build_model() -> tf.keras.models.Sequential:
    model = tf.keras.models.Sequential(
        [
            tf.keras.layers.Flatten(input_shape=(28, 28)),
            tf.keras.layers.Dense(128, activation="relu"),
            tf.keras.layers.Dense(10),
        ]
    )
    model.compile(
        optimizer="adam",
        loss=tf.keras.losses.SparseCategoricalCrossentropy(from_logits=true),
        metrics=["accuracy"],
    )
    return model


def evaluate(model: tf.keras.models.Sequential, test_images, test_labels) -> str:
    test_loss, test_acc = model.evaluate(test_images, test_labels, verbose=2)
    return f"\nTest accuracy: {test_acc}"


def plot_img(i, predictions_arr, true_label, img) -> None:
    true_label, img = true_label[i], img[i]
    plt.grid(false)
    plt.xticks([])
    plt.yticks([])

    plt.imshow(img)

    predicted_label = np.argmax(predictions_arr)
    color = "blue" if predicted_label == true_label else "red"
    plt.xlabel(f"{class_names[predicted_label]} {100 * np.max(predictions_arr):0.2f} ({class_names[true_label]})",
               color=color)


def plot_value_array(i, predictions_arr, true_label) -> None:
    _true_label = true_label[i]
    plt.grid(false)
    plt.xticks(range(10))
    plt.yticks([])
    this_plot = plt.bar(range(10), predictions_arr, color="#777777")
    plt.ylim([0, 1])
    predicted_label = np.argmax(predictions_arr)

    this_plot[predicted_label].set_color("red")
    this_plot[true_label].set_color("blue")


def show_plot(predictions, test_labels, test_images) -> None:
    # Plot the first X test images, their predicted labels, and the true labels.
    # Color correct predictions in blue and incorrect predictions in red.
    num_rows = 5
    num_cols = 3
    num_images = num_rows * num_cols
    plt.figure(figsize=(2 * 2 * num_cols, 2 * num_rows))
    for i in range(num_images):
        plt.subplot(num_rows, 2 * num_cols, 2 * i + 1)
        plot_img(i, predictions[i], test_labels, test_images)
        plt.subplot(num_rows, 2 * num_cols, 2 * i + 2)
        plot_value_array(i, predictions[i], test_labels)
    plt.tight_layout()
    plt.show()


def main() -> None:
    fashion_mnist = tf.keras.datasets.fashion_mnist

    (train_images, train_labels), (test_images, test_labels) = fashion_mnist.load_data()

    train_images = train_images / 255.0
    test_images = test_images / 255.0

    model = build_model()
    model.fit(train_images, train_labels, epochs=10)
    print(evaluate(model, test_images, test_labels))
    probability_model = tf.keras.models.Sequential([model, tf.keras.layers.Softmax()])
    predictions = probability_model.predict(test_images)

    show_plot(predictions, test_labels, test_images)


if __name__ == "__main__":
    main()
