pipeline {
    agent any

    stages {
        stage('Build') {
            steps {
                echo 'Building the project...'
                bat 'dotnet restore'
                bat 'dotnet build'
            }
        }

        stage('Test') {
            steps {
                echo 'Running tests...'
                bat 'dotnet test'
            }
        }
    }

    post {
        success {
            echo 'Build and Test Successful!'
        }
        failure {
            echo 'Build Failed!'
        }
    }
}