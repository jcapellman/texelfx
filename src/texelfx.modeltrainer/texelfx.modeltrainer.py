import tensorflow as tf

def main():
    real_size = (32, 32, 3)
    z_size = 100
    learning_rate = 0.0003

    tf.reset_default_graph()
    
    input_real = tf.placeholder(tf.float32, (None, *real_size), name='input_real')
    
    saver = tf.train.Saver()

    with tf.Session() as sess:
        sess.run(tf.global_variables_initializer())

    saver.save(sess, 'texelfx.ckpt')

if __name__ == '__main__':
    main()
