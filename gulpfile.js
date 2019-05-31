var gulp = require('gulp');


gulp.task('watch', function () {
    //atches for static file changes and copies them to the videolizer App_plugin Directory
    gulp.watch('Videolizer/App_Plugins/**/*', function (done) {
        gulp.src('Videolizer/App_Plugins/Videolizer/**/*')
            .pipe(gulp.dest('Umbraco8/App_Plugins/Videolizer'));
        done();
    });
}); 


gulp.task('default', gulp.series('watch'));