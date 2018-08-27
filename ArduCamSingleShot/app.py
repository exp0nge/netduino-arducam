from flask import Flask, request


app = Flask(__name__)

@app.route('/upload/', methods=['POST'])
def upload():
    print(request.data)
    print(request.files)
    #with open('capture.jpg', 'wb') as f:
    #    f.write(request.data)
    #request.files['filename']
    return 'OK, saved file'


if __name__ == '__main__':
    app.run(debug=True, host='0.0.0.0')
