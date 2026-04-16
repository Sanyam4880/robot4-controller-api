pipeline {
    agent any

    environment {
        SONAR_TOKEN = credentials('sonarcloud-token')
    }

    stages {

        stage('Build') {
            steps {
                echo 'Restoring dependencies...'
                bat 'dotnet restore'

                echo 'Building solution...'
                bat 'dotnet build SIT31314.2C.sln -c Release'

                echo 'Publishing API artifact...'
                bat 'if exist build_artifact rmdir /S /Q build_artifact'
                bat 'dotnet publish robot4-controller-api.csproj -c Release -o build_artifact'

                echo 'Artifact files:'
                bat 'dir build_artifact'
            }
        }

        stage('Test') {
            steps {
                echo 'Running tests on solution (NOT artifact)...'
                
            
                bat 'dotnet test SIT31314.2C.sln --no-build'
            }
        }

        stage('Code Quality') {
            steps {
                echo 'Running SonarCloud analysis...'

                bat """
                dotnet sonarscanner begin /k:"dc73124029ab9bc22fd8a3d32a219cdf9c72808e" /o:"YOUR_ORG_NAME" /d:sonar.login=%SONAR_TOKEN%
                dotnet build SIT31314.2C.sln
                dotnet sonarscanner end /d:sonar.login=%SONAR_TOKEN%
                """
            }
        }
    }

    post {
        success {
            echo 'Pipeline SUCCESS: Build + Test + Code Quality passed!'
        }
        failure {
            echo 'Pipeline FAILED!'
        }
    }
}